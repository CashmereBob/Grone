using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Grone.MVC.ViewModel
{
   public class AddUserViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Please type in username")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Please type in a password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please retype your password")]
        
        public string RePassword { get; set; }
        [Required(ErrorMessage = "Please type in your email")]
        public string Email { get; set; }
    }
}
