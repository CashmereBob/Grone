using Grone.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grone.Data.IRepository
{
    public interface IUserRepository
    {
        void Add(UserEntityModel entity);
        void Delete(PostEntityModel entity);
        void Delete(CommentEntityModel entity);
        List<UserEntityModel> GetAll();
        UserEntityModel GetUserByMail(string eMail);
        UserEntityModel GetUserById(Guid id);
        void Update(UserEntityModel entity);
        UserEntityModel CheckCredentials(string email, string password);
        bool EmailAlreadyExists(string eMail);
    }
}
