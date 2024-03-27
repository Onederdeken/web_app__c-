using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Application2.Models;
using Microsoft.AspNetCore.Authorization;
using db;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity;
using localstorage;
using System.Web;
namespace Application2.Controllers;

using System.Net;
using DATA;
using Manager;
[ApiController]
[Route("[controller]")]
public class VhodController:Controller{
    private  Context _DB;
    public VhodController(Context DB)
    {
        _DB = DB;
    }
    [Route("Regist")]
    public IActionResult Vhod(){

        return View();
    }
    [HttpPost]
    [Route("Regist")]
    public async Task<IActionResult> Vhod([FromForm]  Manager.ManagerUsers user){

        User User = _DB.Users.FirstOrDefault(c=>c.Email == user.Email);
        if(User!=null){
            if(User.Password == user.Password){
                CookieOptions options = new(){Domain = "localhost", Expires = DateTime.Now.AddYears(1), Path = "/"};
                HttpContext.Response.Cookies.Append("id", Convert.ToString(User.Id), options);
                HttpContext.Response.Cookies.Append("role", User.role, options);
                return RedirectToRoute(new{ controller="Home", action="Index"});
            }
            else{
                ViewData["ErrorMesage"]= "Неверный Пароль";
                return View();
            }
        } 
        else{
            ViewData["ErrorMesage"]= "Неверный Email";
            return View();
    
        }

        
    }

    
}