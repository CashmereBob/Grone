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
            using (var context = new GroneEntities())
            {
                var newAdmin = new UserEntityModel()
                {
                    eMail = entity.eMail,
                    Fullname = entity.Fullname,
                    Password = entity.Password,
                };

                // todo: check if email already exists

                context.Users.Add(newAdmin);

                context.SaveChanges();
            }
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
