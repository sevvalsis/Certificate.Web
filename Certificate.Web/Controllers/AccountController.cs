using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Certificate.Web.Models;
using Certificate.Web.ViewModel;
using DataAccessLayer.Concrete;
using EntityLayer;

public class AccountController : Controller
{
    private readonly Context _context;

    public AccountController(Context context)
    {
        _context = context;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(LoginViewModel model)
    {
        var user = _context.Users.FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);
        if (user != null)
        {
         
            HttpContext.Session.SetString("Username", user.Username);
            return RedirectToAction("Index", "Admin"); 
        }
        ViewBag.Message = "Geçersiz kullanıcı adı veya şifre!";
        return View();
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}
