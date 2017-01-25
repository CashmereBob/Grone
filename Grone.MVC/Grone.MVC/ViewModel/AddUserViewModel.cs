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
        [MaxLength(30, ErrorMessage = "Can only take 1-30 characters")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Please type in a password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please retype your password")]
        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        public string RePassword { get; set; }
        [Required(ErrorMessage = "Please type in your email")]
        [EmailAddress(ErrorMessage = "Must be a valid emailform")]
        public string Email { get; set; }
    }
}
