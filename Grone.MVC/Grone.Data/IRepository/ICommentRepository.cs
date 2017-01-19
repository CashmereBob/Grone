using Grone.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grone.Data.IRepository
{
    public interface ICommentRepository//repo
    {
        void Add(CommentEntityModel comment);
        void Delete(Guid id);
        void Delete(string id);
        CommentEntityModel GetById(Guid id);
        CommentEntityModel GetById(string id);
        IEnumerable<CommentEntityModel> GetByPostId(Guid id);
        IEnumerable<CommentEntityModel> GetByPostId(string id);
        CommentEntityModel GetByCommentId(Guid id);
        CommentEntityModel GetByCommentId(string id);
    }
}
