using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Grone.MVC.ViewModel
{
    public class PostViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImgSrc { get; set; }
        public DateTime Date { get; set; }
        public int TimeLeft{ get; set; }
        public int TimeAdded { get; set; }
        public string MemberId { get; set; }
        public IEnumerable<CommentViewModel> Comments { get; set; }
    }
}