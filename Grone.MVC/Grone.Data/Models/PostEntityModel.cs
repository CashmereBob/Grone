using System;
using System.Collections.Generic;

namespace Grone.Data.Models
{
    class PostEntityModel
    {
        public Guid Id { get; set; }
        public DateTime Uploaded { get; set; }
        public string ImgSrc { get; set; }
        public int TimeLeft { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int TimeAdded { get; set; }
        public int TotalAdded { get; set; }
        public string MemberId { get; set; }
        public virtual IEnumerable<CommentEntityModel> Comments { get; set; }

        public PostEntityModel()
        {
            Comments = new HashSet<CommentEntityModel>();
            Id = Guid.NewGuid();
        }
    }
}
