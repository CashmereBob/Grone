using Grone.Data.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grone.Data.Models;

namespace Grone.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        public void Add(UserEntityModel entity)
        {
            // todo ivan: dont check for existing email here!

            using (var context = new GroneEntities())
            {
                if (EmailAlreadyExists(entity.eMail))
                {

                }
                else
                {
                    var newAdmin = new UserEntityModel()
                    {
                        eMail = entity.eMail,
                        Fullname = entity.Fullname,
                        Password = entity.Password,
                    };

                    context.Users.Add(newAdmin);

                    context.SaveChanges();
                }
            }
        }

        public void Delete(CommentEntityModel entity)
        {
            ICommentRepository repo = new CommentRepository();

            using (var context = new GroneEntities())
            {
                repo.Delete(entity.Id);

                context.SaveChanges();
            }
        }

        public void Delete(PostEntityModel entity)
        {
            IPostRepository repo = new PostRepository();

            using (var context = new GroneEntities())
            {
                repo.Delete(entity.Id);

                context.SaveChanges();
            }
        }

        public List<UserEntityModel> GetAll()
        {
            using (var context = new GroneEntities())
            {
                return context.Users.ToList();
            }
        }

        public UserEntityModel GetUserById(Guid id)
        {
            using (var context = new GroneEntities())
            {
                return context.Users.FirstOrDefault(u => u.Id == id);
            }
        }

        public UserEntityModel CheckCredentials(string email, string password)
        {
            using (var context = new GroneEntities())
            {
                foreach (var user in context.Users)
                {
                    if (user.eMail == email && user.Password == password)
                        return user;
                }
                return null;
            }
        }

        public UserEntityModel GetUserByMail(string eMail)
        {
            using (var context = new GroneEntities())
            {
                return context.Users.FirstOrDefault(u => u.eMail == eMail);
            }
        }

        public void Update(UserEntityModel entity)
        {
            // todo ivan: dont check for existing email here!

            using (var context = new GroneEntities())
            {
                var userToUpdate = context.Users.FirstOrDefault(u => u.Id == entity.Id);

                /*if email is already taken, 
                update everything except the email*/
                if (EmailAlreadyExists(entity.eMail))
                {
                    userToUpdate.Fullname = entity.Fullname;
                    userToUpdate.Password = entity.Password;
                }
                else
                {
                    userToUpdate.eMail = entity.eMail;
                    userToUpdate.Fullname = entity.Fullname;
                    userToUpdate.Password = entity.Password;
                }

                context.SaveChanges();
            }
        }

        public bool EmailAlreadyExists(string eMail)
        {
            using (var context = new GroneEntities())
            {
                foreach (var user in GetAll())
                {
                    if (user.eMail == eMail)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
    }
}
