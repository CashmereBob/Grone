using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Grone.MVC.ViewModel
{
    public class UpdateUserViewModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}