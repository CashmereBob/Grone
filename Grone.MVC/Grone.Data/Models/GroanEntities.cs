using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grone.Data.Models
{
    public class GroanEntities:DbContext
    {
        DbSet<CommentEntityModel> Comments { get; set; }
        DbSet<PostEntityModel> Posts { get; set; }
    }
}
