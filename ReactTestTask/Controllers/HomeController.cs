using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReactTestTask.Models;
using ReactTestTask.ViewModels;

namespace ReactTestTask.Controllers
{
    public class HomeController : Controller
    {
        private PostgreSQLContext _dbContext;

        public HomeController(PostgreSQLContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {            
            return View(_dbContext.Users);
        }

        [Route("Users")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Users()
        {
            List<UserViewModel> users = new List<UserViewModel>();
            foreach(User user in _dbContext.Users)
            {
                users.Add(new UserViewModel(user));
            }
            return Json(users);
        }

        [Route("Users/Add")]
        [HttpPost]
        public IActionResult AddUser(User user)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Users.Add(user);
                _dbContext.SaveChangesAsync();
                return Content("YES, YES!!!");
            }
            return Content("Мы обосрались");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("Metrics")]
        public IActionResult Metrics()
        {
            return View();
        }
    }
}
