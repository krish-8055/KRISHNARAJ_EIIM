using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LMS.Models;

namespace LMS.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Login(LoginModel employee)
    {
        Repository repository=new Repository();
        bool managerValid=repository.IsValidManager(employee);
        bool employeeValid=repository.IsValidEmployee(employee);
        if(managerValid)
        {
            return RedirectToAction("ManagerHome","Manager");
        }
        else if(employeeValid)
        {       
            HttpContext.Session.SetString("UserName",Convert.ToString(Repository.GetID(employee.Username)));
            return RedirectToAction("UserHome","User"); 
        }        
        else
        {        
            ViewBag.InvalidUserMessage="Invalid Username/Password";
            return View();
        }
    }   

    public IActionResult Signup()
    {
        return View();
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();

        return RedirectToAction("Login","Home");
    }
    
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}
