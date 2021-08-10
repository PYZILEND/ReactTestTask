using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactTestTask.Models
{
    public static class RollingRetentionCalculator
    {
        public static int CalculateRollingRetention(List<User> users, int day)
        {
            float usersBeforeDayX = users.Where(u => (DateTime.Now.Date - u.DateRegestration.Date).TotalDays >= day).Count();
            if(usersBeforeDayX <= 0)
            {
                return 0;
            }
            float retainedUsers = users.Where(u => u.DateRegestration.AddDays(day).Date <= u.DateLastVisit.Date).Count();
            float retention = retainedUsers / usersBeforeDayX * 100;
            return (int)retention;
        }
    }
}