﻿using Grone.Data.IRepository;
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
                context.Users.Add(entity);

                context.SaveChanges();
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
            using (var context = new GroneEntities())
            {
                var userToUpdate = context.Users.FirstOrDefault(u => u.Id == entity.Id);

                userToUpdate.eMail = entity.eMail;
                userToUpdate.Fullname = entity.Fullname;
                userToUpdate.Password = entity.Password;

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
