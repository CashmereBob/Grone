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

        [Required(ErrorMessage ="email is required for login")]
        public string eMail { get; set; }
        [Required(ErrorMessage = "password is required for login")]
        public string Password { get; set; }
        public string Fullname { get; set; }
    }
}