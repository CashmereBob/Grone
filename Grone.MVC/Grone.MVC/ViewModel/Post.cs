﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Grone.MVC.ViewModel
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImgSrc { get; set; }

        public DateTime Date { get; set; }
        public int TimeLeft{ get; set; }
        public int TimeAdded { get; set; }
        public int TotalAdded { get; set; }
        public Guid MemberId { get; set; }
        public IEnumerable<CommentViewModel> Comments { get; set; }
    }
}