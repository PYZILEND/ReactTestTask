using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReactTestTask.Models;
using ReactTestTask.ViewModels;
using System.Text.RegularExpressions;

namespace ReactTestTask.Controllers
{
    public class HomeController : Controller
    {
        private PostgreSQLContext _dbContext;
        const int RollingRetentionDay = 7;

        public HomeController(PostgreSQLContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            List<User> users = _dbContext.Users.ToList();
            HomeViewModel viewModel = new HomeViewModel(users, RollingRetentionDay);
            return View(viewModel);
        }

        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult GetUsers()
        {            
            List<UserViewModel> users = new List<UserViewModel>();
            foreach(User user in _dbContext.Users)
            {
                users.Add(new UserViewModel(user));
            }
            return Json(users);
        }

        [HttpPost]
        public IActionResult PostUsers([FromBody]List<User> users)
        {
            bool[] validityIndexes = new bool[users.Count];
            for (int i = 0; i < validityIndexes.Length; i++)
            {
                validityIndexes[i] = true;
            }

            if (ModelState.IsValid)
            {
                _dbContext.Users.UpdateRange(users);
                _dbContext.SaveChanges();
                return Json(new { success = true, validityIndexes = validityIndexes });
            }
            else
            {                
                foreach (var key in ModelState.Keys)
                {
                    string keyIndex = Regex.Match(key, "(?<=\\[)\\d*(?=\\])").Value;
                    validityIndexes[int.Parse(keyIndex)] = false;
                }                
                return Json(new { success = false, validityIndexes = validityIndexes });
            }
        }
        
        public IActionResult GetMetrics()
        {
            List<User> users = _dbContext.Users.ToList();
            int rollingRetention = RollingRetentionCalculator.CalculateRollingRetention(users, RollingRetentionDay);
            int[] lifetiemeDistribution = UsersLifetimeCalculator.CalculateUsersLifetimeDistribution(users);
            return Json(new { rollingRetention = rollingRetention, lifetiemeDistribution = lifetiemeDistribution});
        }
    }
}
