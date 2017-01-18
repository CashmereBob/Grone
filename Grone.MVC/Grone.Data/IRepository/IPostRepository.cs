using Grone.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grone.Data.IRepository
{
    public interface IPostRepository
    {
        void AddOrUpdate(PostEntityModel post);
        void Delete(Guid id);
        void Delete(string id);
        PostEntityModel GetById(Guid id);
        PostEntityModel GetById(string id);
        IEnumerable<PostEntityModel> GetAll();
        IEnumerable<CommentEntityModel> GetTop3Comments(PostEntityModel post);
    }
}
