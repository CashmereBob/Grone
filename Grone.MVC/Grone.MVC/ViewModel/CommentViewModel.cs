using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Grone.MVC.ViewModel
{
    public class CommentViewModel
    {
        public Guid id { get; set; }
        public string Comment  { get; set; }
        public DateTime Date { get; set; }
        public Guid Kommentarid { get; set; }
        public string Member { get; set; }
        public string ImgSrc { get; set; }
    }
}