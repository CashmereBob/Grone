﻿using Grone.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grone.Data.IRepository
{
    public interface IUserRepository
    {
        void Add(UserEntityModel entity);//check for existsing email
        void Delete(PostEntityModel entity);
        void Delete(CommentEntityModel entity);
        IEnumerable<UserEntityModel> GetAll();
        UserEntityModel GetUser(Guid id);
        void Update(UserEntityModel entity); //check for existsing email
    }
}