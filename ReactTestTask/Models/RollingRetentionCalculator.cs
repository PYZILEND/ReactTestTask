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
            int usersBeforeDayX = users.Where(u => (DateTime.Now.Date - u.DateRegestration.Date).TotalDays >= day).Count();
            int retainedUsers = users.Where(u => u.DateRegestration.AddDays(day).Date < u.DateLastVisit.Date).Count();
            return (int)(retainedUsers / retainedUsers * 100);
        }
    }
}