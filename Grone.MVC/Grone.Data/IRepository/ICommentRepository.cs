﻿using Grone.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grone.Data.IRepository
{
    interface ICommentRepository//repo
    {
        void AddOrUpdate();
        void Delete(Guid id);
        void Delete(string id);
        CommentEntityModel GetById(Guid id);
        CommentEntityModel GetById(string id);
        CommentEntityModel GetByPostId(Guid id);
        CommentEntityModel GetByPostId(string id);
        CommentEntityModel GetByCommentId(Guid id);
        CommentEntityModel GetByCommentId(string id);
    }
}