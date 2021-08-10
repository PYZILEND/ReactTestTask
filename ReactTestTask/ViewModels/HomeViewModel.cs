using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReactTestTask.Models;

namespace ReactTestTask.ViewModels
{
    public class HomeViewModel
    {
        public List<UserViewModel> Users { get; set; }
        public int RollingRetention { get; set; }
        public int[] LifetiemeDistribution { get; set; }

        public HomeViewModel(List<User> modelUsers, int day)
        {
            List<UserViewModel> vmUsers = new List<UserViewModel>();
            foreach (User user in modelUsers)
            {
                vmUsers.Add(new UserViewModel(user));
            }
            RollingRetention = RollingRetentionCalculator.CalculateRollingRetention(modelUsers, day);
            LifetiemeDistribution = UsersLifetimeCalculator.CalculateUsersLifetimeDistribution(modelUsers);
            Users = vmUsers;
        }
    }
}