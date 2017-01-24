using System;
using System.Collections.Generic;

namespace Grone.Data.Models
{
    public class PostEntityModel
    {
        public Guid Id { get; set; }
        public DateTime Uploaded { get; set; }
        public string ImgSrc { get; set; }
        public int TimeLeft { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int TotalTimeAdded { get; set; }
        public string MemberId { get; set; }
        public virtual IEnumerable<CommentEntityModel> Comments { get; set; }

        public PostEntityModel()
        {
            Comments = new HashSet<CommentEntityModel>();
            Id = Guid.NewGuid();
            Uploaded = DateTime.Now;
            TimeLeft = 120;
            TotalTimeAdded = 120;
        }
        public PostEntityModel(Guid id)
        {
            Comments = new HashSet<CommentEntityModel>();
            Id = id;
            Uploaded = DateTime.Now;
            TimeLeft = 120;
            TotalTimeAdded = 120;
        }
    }
}
