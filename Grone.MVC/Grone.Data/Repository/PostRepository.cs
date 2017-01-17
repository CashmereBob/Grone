using Grone.Data.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grone.Data.Models;

namespace Grone.Data.Repository
{
    public class PostRepository : IPostRepository
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

        public IEnumerable<PostEntityModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public PostEntityModel GetById(string id)
        {
            throw new NotImplementedException();
        }

        public PostEntityModel GetById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
