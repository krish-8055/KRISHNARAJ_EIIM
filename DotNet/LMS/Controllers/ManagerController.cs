using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LMS.Models;

namespace LMS.Controllers;

public class ManagerController : Controller
{

    public IActionResult ManagerHome()
    {
        return View();
    }

    public IActionResult RequestedLeaves()
    {
        return View();
    }

    

    public IActionResult ViewTeamDetails()
    {
        return View();
    }

    public IActionResult MailAdmin()
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