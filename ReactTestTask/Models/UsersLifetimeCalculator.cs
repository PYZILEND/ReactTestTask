using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactTestTask.Models
{
    public static class UsersLifetimeCalculator
    {
        public static int[] CalculateUsersLifetimeDistribution(List<User> users)
        {
            if(users.Count <= 0)
            {
                return new int[] { 0};
            }
            int maxLifetime = (int)users.Max(u => (u.DateLastVisit - u.DateRegestration).TotalDays);
            int[] distribution = new int[maxLifetime+1];
            foreach(User user in users)
            {
                int lifetime = (int)(user.DateLastVisit - user.DateRegestration).TotalDays;
                distribution[lifetime]++;
            }
            return distribution;
        }
    }
}