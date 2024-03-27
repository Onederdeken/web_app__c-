using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Application2.Models;
using Microsoft.AspNetCore.Authorization;
using db;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity;
using Application2.DATA;
using System.Drawing;
using System.IO.MemoryMappedFiles;
using Application2.Manager;
using Microsoft.EntityFrameworkCore;
using System.Buffers;
namespace Application2.Controllers;
[ApiController]
[Route("[controller]")]
public class AccauntController:Controller{
    private  Context _DB;
    IWebHostEnvironment _appEnvironment;
    public AccauntController(Context DB, IWebHostEnvironment appEnvironment)
    {
        _DB = DB;
        _appEnvironment = appEnvironment;
    }
    [Route("Accaunt")]
    public async Task<IActionResult> Accaunt(){
        if(data.ID == "0"){
            ViewData["Message"]="Войдите в акаунт";
            return View();
        }
        else{
            User? user = await _DB.Users.FindAsync(Guid.Parse(data.ID));
            if(user.role == "Person"){
                return RedirectToAction("AccauntForPerson");
            }
            else if(user.role == "Seller"){
                return RedirectToAction("AccauntForSeller"); 
            }
            else{
                throw new Exception("Войдите в аккаунт");
            }

        }
    }
    [Route("AccauntForPerson")]
    public async Task<IActionResult> AccauntForPerson(){
        
        Person? person = await _DB.Persons.FindAsync(Guid.Parse(data.ID));
        return View(person);
        //Person person = new Person(){NickName="andrew", Password="AFGRTE456", Telephone="+7-953-759-53-49", Email="kapitovsiy@mail.ru"};
        
    }
    [Route("AccauntForSeller")]
    public async Task<IActionResult> AccauntForSeller(){
        Seller? seller = await _DB.Sellers.FindAsync(Guid.Parse(data.ID));
        return View(seller); 
    }
    [Route("OutAcc")]
    public async Task<IActionResult> OutAcc(){
        HttpContext.Response.Cookies.Delete("id");
        HttpContext.Response.Cookies.Delete("role");
        data.ID="0";
        data.role ="";
        return RedirectToRoute(new {Controller="Home", Action="Index"});
    }
    [Route("Cart")]
    public async Task<IActionResult> Cart(){
        Person? item =_DB.Persons.First(d=>d.Id==Guid.Parse(data.ID));
        await _DB.Entry(item).Collection(p=>p.Products).LoadAsync();
        return View(item.Products);
    }
    [Route("ListProducts")]
    public async Task<IActionResult> ListProducts(){
        Seller person =  _DB.Sellers.Find(Guid.Parse(data.ID));

        await _DB.Entry(person).Collection(c=>c.Products).LoadAsync();
        
        
        return View(person.Products);
        
       
    }
    [Route("AddProducts")]
    [HttpGet]
    public async Task<IActionResult> AddProducts(){
        return View();
    }

    [Route("AddProducts")]
    [HttpPost]
    public async Task<IActionResult> AddProducts([FromForm] ManagerProduct? product, IFormFile? fileBase){
        if(fileBase!=null){
            int len=fileBase.ContentType.Length;
            string filename = Convert.ToString(Guid.NewGuid());
            string path = "/img/img_products/"+filename;
            using (var fileStream = new FileStream(_appEnvironment.WebRootPath+path+".png", FileMode.Create)){              
                await fileBase.CopyToAsync(fileStream);
                fileStream.Close();
                
            }
            if(product == null){
                ViewData["massage_error"]="Запоните все поля";
                return View();
            }
            product.DirectoryPhotoProduct=_appEnvironment.WebRootPath+path+".png";
            string role = Request.Form["role"];
            if(role == null){
                ViewData["massage_error"]="Запоните все поля";
                return View();
            }
            Category category = _DB.Categories.Find(role);
            Seller seller = await _DB.Sellers.FindAsync(Guid.Parse(data.ID));
            
            product.SellerId=seller.Id;
            product.Seller=seller;
            Product tovar = new Product(){Name=product.Name, DirectoryPhotoProduct=product.DirectoryPhotoProduct, Description=product.Description, CategoryNameCategory=role, cost= (int)product.cost, Category=category, Seller=seller, SellerId=seller.Id};
            await _DB.Products.AddAsync(tovar);
            seller.Products.Add(tovar);
            await _DB.SaveChangesAsync();
            
            return RedirectToRoute(new {Controller="Home", Action="Index"});
            
        }
        else{
            ViewData["massage_error"] = "Заполните все поля";
            return View();
        }
    }
    [Route("AddMoney")]
    [HttpPost]
    public async Task<IActionResult> AddMoney(){
        long money = Convert.ToInt64(Request.Form["Money"]);
        Person person = _DB.Persons.Find(Guid.Parse(data.ID));
        person.CMoney+=(int)money;
        await _DB.SaveChangesAsync();
        return RedirectToAction("Accaunt");
    }
    [Route("Cart")]
    [HttpPost]
    public async Task<IActionResult> Cart([FromForm] string IdProduct){
        
        Product? Products = _DB.Products
            .Include(c=>c.Seller)
            .Include(c=>c.Person)
                .ThenInclude(c=>c.Products).First(c=>c.Id == Guid.Parse(IdProduct));
        if(Products.Person.Find(c=>c.Id == Guid.Parse(data.ID)).CMoney < Products.cost){
            Person? item =_DB.Persons.First(d=>d.Id==Guid.Parse(data.ID));
            await _DB.Entry(item).Collection(p=>p.Products).LoadAsync();
            ViewData["massage"] = "Нехватае средств на счету";
            ViewBag.ID = Products.Id;
            return View(item.Products);
        }
        Products.Seller.CountMoney += Products.cost;
        Products.Person.Find(c=>c.Id == Guid.Parse(data.ID)).CMoney -= Products.cost;
        FileInfo file = new FileInfo(Products.DirectoryPhotoProduct);
        file.Delete();
        Products.Person.Find(c=>c.Id == Guid.Parse(data.ID)).Products.Remove(Products);
        Products.Seller.Products.Remove(Products);
        _DB.Products.Remove(Products);
        await _DB.SaveChangesAsync();
        return RedirectToAction("Cart");
    }
    [Route("ChangeProfile")]
    [HttpGet]
    public async Task<IActionResult> ChangeProfile(){
        User user = await _DB.Users.FindAsync(Guid.Parse(data.ID));
        return View(user);
    }
    [Route("ChangeProfile")]
    [HttpPost]
    public async Task<IActionResult> ChangeProfile([FromForm] User user){
        _DB.Entry(user).State= Microsoft.EntityFrameworkCore.EntityState.Modified;
        if(user.role == "Person"){
            Person person = _DB.Persons.Find(Guid.Parse(data.ID));
            person.NickName = user.NickName;
            person.Password = user.Password;
            //_DB.Entry(person).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _DB.SaveChangesAsync();
            return RedirectToAction("AccauntForPerson");
        }
        else{
            Seller person = _DB.Sellers.Find(Guid.Parse(data.ID));
            person.NickName = user.NickName;
            person.Password = user.Password;
            _DB.Entry(person).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _DB.SaveChangesAsync();
            return RedirectToAction("AccauntForSeller");

        }
        
    }
    
    [Route("ChangePhoto")]
    public async Task<IActionResult> ChangePhoto([FromForm] IFormFile filebase){
        User user = await _DB.Users.FindAsync(Guid.Parse(data.ID));
        if(filebase!=null){
            int len=filebase.ContentType.Length;
            string filename = Convert.ToString(Guid.NewGuid());
            string path = "/img/img_users/"+filename;
            using (var fileStream = new FileStream(_appEnvironment.WebRootPath+path+".png", FileMode.Create)){              
                await filebase.CopyToAsync(fileStream);
                fileStream.Close();
                
            }
            if(user.DirectoryPhotoUser != "/home/user/Source/test_drive/Application2/wwwroot/img/no_avatar.webp"){
                FileInfo file = new FileInfo(user.DirectoryPhotoUser);
                file.Delete();
            }
            user.DirectoryPhotoUser = _appEnvironment.WebRootPath+path+".png";
        }
        else{
            user.DirectoryPhotoUser="/home/user/Source/test_drive/Application2/wwwroot/img/no_avatar.webp";
        }
        if(user.role == "Person"){
            Person person = _DB.Persons.Find(Guid.Parse(data.ID));
            person.DirectoryPhotoUser = user.DirectoryPhotoUser;
            //_DB.Entry(person).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _DB.SaveChangesAsync();
            return RedirectToAction("AccauntForPerson");
        }
        else{
            Seller person = _DB.Sellers.Find(Guid.Parse(data.ID));
            person.DirectoryPhotoUser = user.DirectoryPhotoUser;
            _DB.Entry(person).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _DB.SaveChangesAsync();
            return RedirectToAction("AccauntForSeller");

        }

    }
    [HttpPost]
    public async Task<IActionResult> DeleteProduct([FromForm] string ID){
        if(data.role == "Seller"){
            Product product = await _DB.Products.FindAsync(Guid.Parse(ID));
            FileInfo file = new FileInfo(product.DirectoryPhotoProduct);
            file.Delete();
            await _DB.Entry(product).Collection(p=>p.Person).LoadAsync();
            await _DB.Entry(product).Reference(p=>p.Seller).LoadAsync();
            _DB.Products.Remove(product);
            await _DB.SaveChangesAsync();
            return RedirectToAction("ListProducts");
        }
        else{
            Person person = await _DB.Persons.FindAsync(Guid.Parse(data.ID));
            await _DB.Entry(person).Collection(c=>c.Products).LoadAsync();
            Product product = await _DB.Products.FindAsync(Guid.Parse(ID));
            person.Products.Remove(product);
            await _DB.SaveChangesAsync();
            return RedirectToAction("Cart");
        }   

    }

}