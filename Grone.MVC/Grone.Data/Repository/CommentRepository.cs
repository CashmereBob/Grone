using Grone.Data.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grone.Data.Models;

namespace Grone.Data.Repository
{
    class CommentRepository : ICommentRepository
    {
        public void AddOrUpdate()
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public CommentEntityModel GetByCommentId(string id)
        {
            throw new NotImplementedException();
        }

        public CommentEntityModel GetByCommentId(Guid id)
        {
            throw new NotImplementedException();
        }

        public CommentEntityModel GetById(string id)
        {
            throw new NotImplementedException();
        }

        public CommentEntityModel GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public CommentEntityModel GetByPostId(string id)
        {
            throw new NotImplementedException();
        }

        public CommentEntityModel GetByPostId(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
