using Grone.Data.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grone.Data.Models;

namespace Grone.Data.Repository
{
    class UserRepository : IUserRepository
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
    }
}
