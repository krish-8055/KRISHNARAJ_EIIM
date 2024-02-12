using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LMS.Models;

namespace LMS.Controllers;

public class UserController : Controller
{
private readonly ILogger<UserController> _logger;

private readonly LmsDBcontext _context;



public IActionResult UserHome()
{
    return View();
}

public IActionResult Complaint()
{
    return View();
}

 [HttpPost]
        public IActionResult Complaint(ComplaintModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var complainObject = new ComplaintModel
                {
                    Email = viewModel.Email,
                    Complaints = viewModel.Complaints,
                   
                };
 
                // Add the entity to the DbContext and save changes to the database
                _context.ComplaintTable.Add(complainObject);
                _context.SaveChanges();

                ViewBag.ErrorMessage = "Complaint Filed";
                // Redirect to a different page after successful addition
                return View(); // Change "Index" and "Home" to your desired destination
            }
 
            // If ModelState is not valid, return to the same page or show an error message
            ViewBag.ErrorMessage = "Complaint Filed Error";
            return View();
        }

[HttpGet]
public IActionResult RequestLeave()
{
    RequestLeaveModel request = new RequestLeaveModel();
    request.EmpId=Convert.ToInt32(HttpContext.Session.GetString("UserName"));
    return View(request);
}

[HttpPost]
public IActionResult RequestLeave(RequestLeaveModel request)
    {
        System.Console.WriteLine("add Reaquest Leaves entered");
        Repository.RequestLeave(request);
        ViewBag.message="Requested Leave to Manager";
        return View("RequestLeave");
        
     }

[HttpGet]
public IActionResult ViewLeave()
{
    List<RequestLeaveModel> EmpDetails = Repository.GetRequestLeave();

    return View(EmpDetails);
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
    public UserController(ILogger<UserController> logger,LmsDBcontext dbContext)
    {
        _logger = logger;
        _context = dbContext;
    }

}