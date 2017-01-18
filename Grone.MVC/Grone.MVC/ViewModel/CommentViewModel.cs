using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Grone.MVC.ViewModel
{
    public class CommentViewModel
    {
        public Guid Id { get; set; }
        public string Comment  { get; set; }
        public DateTime Date { get; set; }
        public Guid CommentId { get; set; }
        public string MemberId { get; set; }
        public string ImgSrc { get; set; }
    }
}