using Grone.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grone.Data.IRepository
{
    interface IPostRepository
    {
        void AddOrUpdate();
        void Delete(Guid id);
        void Delete(string id);
        PostEntityModel GetById(Guid id);
        PostEntityModel GetById(string id);
        IEnumerable<PostEntityModel> GetAll();
    }
}
