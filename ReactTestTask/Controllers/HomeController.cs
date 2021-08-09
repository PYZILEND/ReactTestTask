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
            List<UserViewModel> users = new List<UserViewModel>();
            foreach (User user in _dbContext.Users)
            {
                users.Add(new UserViewModel(user));
            }
            return View(users);
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

        [Route("Users/Edit")]
        [HttpPost]
        public IActionResult EditUsers([FromBody]List<User> users)
        {            
            if (ModelState.IsValid)
            {
                _dbContext.Users.UpdateRange(users);
                _dbContext.SaveChangesAsync();
                return Content("Все получилось");
            }
            
            return Content("Что-то пошло не так");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("Metrics")]
        public IActionResult Metrics(int day = 7)
        {
            List<User> users = _dbContext.Users.ToList();
            MetricsViewModel viewModel = new MetricsViewModel();
            viewModel.RollingRetention = RollingRetentionCalculator.CalculateRollingRetention(users, day);
            viewModel.LifetiemeDistribution = UsersLifetimeCalculator.CalculateUsersLifetimeDistribution(users);
            return View(viewModel);
        }
    }
}
