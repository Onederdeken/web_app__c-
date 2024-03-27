using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Application2.Models;
using Microsoft.AspNetCore.Authorization;
using db;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity;
namespace Application2.Controllers;
[ApiController]
[Route("[controller]")]
public class RegistrationController:Controller{
    private  Context _DB;
    public RegistrationController(Context DB)
    {
        _DB = DB;
    }
    [Route("Regist")]
    public IActionResult Regist(){
        return View();
    }
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    [Route("Regist")]
    
    public async Task<IActionResult> Regist([FromForm] User user){
       
       
        if (ModelState.IsValid)
        {
            if(!(_DB.Users.Any(c=>c.Telephone == user.Telephone) | _DB.Users.Any(c=>c.Email == user.Email))){
                if(user.role == "Person"){
                    user.DirectoryPhotoUser = "/home/user/Source/test_drive/Application2/wwwroot/img/no_avatar.webp";
                    await _DB.Users.AddAsync(user);
                    await _DB.SaveChangesAsync();
                    Person person = new Person(){Id=user.Id, NickName=user.NickName, Password=user.Password, Email=user.Email, Telephone=user.Telephone, DirectoryPhotoUser = "/home/user/Source/test_drive/Application2/wwwroot/img/no_avatar.webp"};
                    await _DB.Persons.AddAsync(person);
                    await _DB.SaveChangesAsync();
                }
                else{
                    user.DirectoryPhotoUser = "/home/user/Source/test_drive/Application2/wwwroot/img/no_avatar.webp";
                    await _DB.Users.AddAsync(user);
                    await _DB.SaveChangesAsync();
                    Seller seller = new Seller(){Id=user.Id, NickName=user.NickName, Password=user.Password, Email=user.Email, Telephone=user.Telephone, CountMoney=0, DirectoryPhotoUser=user.DirectoryPhotoUser};
                    await _DB.Sellers.AddAsync(seller);
                    await _DB.SaveChangesAsync();
                }
                
                return RedirectToRoute(new { controller="Home", action="Index"});
            }
            else{
                ViewData["ErrorMessage"]= "Такой пользователь уже существует";
                return View();
            }
            
        }
        ViewData["ErrorMessage"]= "Ошибка авторизации попробуйте еще раз";
        return View();
        
    }
}