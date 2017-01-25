using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Grone.MVC.ViewModel
{
    public class UserLoginViewModel
    {
        public Guid Id { get; set; }
        [EmailAddress(ErrorMessage = "Must be a valid emailform")]
        [Required(ErrorMessage ="email is required for login")]
        public string eMail { get; set; }
        [Required(ErrorMessage = "password is required for login")]
        public string Password { get; set; }
        [Required(ErrorMessage = "You have to write in a namme")]
        [MaxLength(30, ErrorMessage ="Can only take 1-30 characters")]
        public string Fullname { get; set; }
    }
}