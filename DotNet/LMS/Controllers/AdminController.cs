using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LMS.Models;

namespace LMS.Controllers;

public class AdminController : Controller
{
     public IActionResult AdminHome()
    {
        return View();
    }
    [HttpGet]
        public IActionResult AdminLogin()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AdminLogin(LoginModel admin)
    {
        Tuple<int,string> result = Repository.AdminLogin(admin);
        if(result.Item1 == 1)
        {
            HttpContext.Session.SetString("UserName", result.Item2); 
            return RedirectToAction("AdminHome");
        }
        else
        {
            ViewBag.message="*Invalid Username or Password";
            return View();
        }
    }
    
    [HttpGet]
        public IActionResult AddEmployee()
    {
        return View();
    }

    [HttpPost]

    public IActionResult AddEmployee(LoginModel Employee)
    {
        System.Console.WriteLine("add admin entered");
        int check = Repository.AddEmployee(Employee);
        if(check==1)
        {
            ViewBag.message="*UserName Or Employee ID already Exist";
            return View("AddEmployee");
        }
        else if(check == 2)
        {
            ViewBag.message="*Check the Password, Dosen't match";
            return View("AddEmployee");
        }
        else
        {
            ViewBag.message="*Employee account added sucessfully";
            return View("AddEmployee");
        }
    }




    [HttpGet]
        public IActionResult AddAdmin()
    {
        return View();
    }

    [HttpPost]

    public IActionResult AddAdmin(LoginModel admin)
    {
        System.Console.WriteLine("add admin entered");
        int check = Repository.AddAdmin(admin);
        if(check==1)
        {
            ViewBag.message="*UserName already Exist";
            return View("AddAdmin");
        }
        else if(check == 2)
        {
            ViewBag.message="*Check the Password, Dosen't match";
            return View("AddAdmin");
        }
        else
        {
            ViewBag.message="*Admin account added sucessfully";
            return View("AddAdmin");
        }
    }

    [HttpGet]
        public IActionResult AddManager()
    {
        return View();
    }

    [HttpPost]

    public IActionResult AddManager(LoginModel Man)
    {
        System.Console.WriteLine("add admin entered");
        int check = Repository.AddManager(Man);
        if(check==1)
        {
            ViewBag.message="*UserName already Exist";
            return View("AddManager");
        }
        else if(check == 2)
        {
            ViewBag.message="*Check the Password, Dosen't match";
            return View("AddManager");
        }
        else
        {
            ViewBag.message="*Admin account added sucessfully";
            return View("AddManager");
        }
    }

    [HttpGet]
    public IActionResult ManageLeaves()
    {
        List<RequestLeaveModel> EmpDetails = Repository.GetRequestLeave();
        return View(EmpDetails);
    }
    [HttpPost]
    public IActionResult ManageLeaves(int id,string status)
    {
        Repository.updateStatus(id,status);
        return RedirectToAction("ManageLeaves","Admin");
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