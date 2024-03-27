
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
using System.Security.Principal;
using Microsoft.EntityFrameworkCore;
namespace Application2.Controllers;
[ApiController]
[Route("[controller]")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly Context _DB;

    static private int count = 0;

    public HomeController(ILogger<HomeController> logger, Context DB)
    {
        _logger = logger;
        _DB = DB;
    }
    [Route("Index")]
    public async Task<IActionResult> Index([FromQuery] string? name,[FromQuery] string? grand)
    {
        Console.WriteLine($"name1={name}");
        Console.WriteLine($"grand1={grand}");
        if(HttpContext.Request.Cookies.ContainsKey("id") & data.ID == "0"){
            data.ID = HttpContext.Request.Cookies["id"];
            data.role=HttpContext.Request.Cookies["role"];
        }
        if(_DB.Products.Any() && name == null && grand == null ){
            Console.WriteLine($"name2={name}");
            Console.WriteLine($"grand2={grand}");
            List<Product>? Product = _DB.Products.ToList();
            return View(Product);
        }
        else if(name != null | grand != null){
            Console.WriteLine($"name3={name}");
            Console.WriteLine($"grand3={grand}");
            if(name != null){
                Console.WriteLine($"name4={name}");
                List<Product> products = _DB.Products.Where(c=>c.Name==name).ToList();
                if(products.Count == 0){
                    ViewData["message"] = "Простите, такого товара нет";
                    return View();
                }
            return View(products);
            }
            else if(grand != null){
                Console.WriteLine($"grand4={grand}");
                List<Product> products = _DB.Products.Where(c=>c.CategoryNameCategory==name).ToList();
                if(products.Count == 0){
                    ViewData["message"] = "Простите, в этой категории товаров нет";
                    return View();
                }
                return View(products);
            }
            else{
                return NotFound();
            }
        }
        else{
            ViewData["message"] = "Простите магазин пока пуст";
            return View();
        }

        
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    [Route("addCart")]
    public async Task<IActionResult> addCart(){
        Guid id = Guid.Parse(Request.Form["Id"]);
        Product product = await _DB.Products.FindAsync(id);
        Person person = await _DB.Persons.Include(c=>c.Products).FirstOrDefaultAsync(d=>d.Id==Guid.Parse(data.ID));
        if(person.Products==null){
            person.Products= new List<Product>();
        }
        if(person.Products.Any(c=>c.Id==id)){
            return RedirectToAction("Index");
        }
        else{
            person.Products.Add(product);
            await _DB.SaveChangesAsync();
            return RedirectToAction("Index");
        }
       
    }
    [HttpPost]
    public async Task<IActionResult> Search([FromForm] string name){
        List<Product> products = _DB.Products.Where(c=>c.Name==name).ToList();  
        return View(products);
    }
}
