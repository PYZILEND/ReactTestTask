using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactTestTask.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]       
        public DateTime DateRegestration { get; set; }

        [Required]
        [LaterThan("DateRegestration")]
        public DateTime DateLastVisit { get; set; }
    }
}
