using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReactTestTask.Models;

namespace ReactTestTask.ViewModels
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string DateRegestration { get; set; }
        public string DateLastVisit { get; set; }

        public UserViewModel(User user)
        {
            UserId = user.UserId;
            DateRegestration = user.DateRegestration.ToString("yyyy-MM-dd");
            DateLastVisit = user.DateLastVisit.ToString("yyyy-MM-dd");
        }
    }
}
