using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using usersdb.Models;

namespace usersdb.Controllers;

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

    public IActionResult Register()
    {

        List<UsersModel> users = new List<UsersModel>();

        using (var db = new UsersContext())
        {
            users = db.Users.ToList();
        }

        TempData["users"] = users;
        return View();
    }

    [HttpPost]
    public IActionResult RemoveUser()
    {



        using (var db = new UsersContext())
        {

            var user = db.Users.Where(u => u.Id >= 1).FirstOrDefault();
            if (user != null)
            {

                db.Remove(user);

                db.SaveChanges();

            }
            return RedirectToAction("Users");
        }


    }

    [HttpPost]
    public IActionResult RegisterPost(UsersModel user)
    {




        using (var db = new UsersContext())
        {


            var checkuser = db.Users.Where(x => x.UserName == user.UserName).FirstOrDefault();
            if (checkuser != null)
            {

                ViewBag.Message = "Exsit";
                    return View("Register");
            }
            else
            {


                db.Add(user);
                db.SaveChanges();
                return RedirectToAction("Register");

            }
            

        }


    }

    public IActionResult Users()
    {

        List<UsersModel> users = new List<UsersModel>();

        using (var db = new UsersContext())
        {
            users = db.Users.ToList();
        }

        TempData["users"] = users;

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
