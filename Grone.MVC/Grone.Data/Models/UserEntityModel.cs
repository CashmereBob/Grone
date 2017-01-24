using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grone.Data.Models
{
    public class UserEntityModel
    {
        public Guid Id { get; set; }
        public string eMail { get; set; }
        public string Fullname { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }

        public UserEntityModel()
        {
            Id = Guid.NewGuid();
            //todo create the salt hash here
        }
    }
}
