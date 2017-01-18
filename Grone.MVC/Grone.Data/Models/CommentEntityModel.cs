﻿using System;

namespace Grone.Data.Models
{
    public class CommentEntityModel
    {
        public Guid Id { get; set; }
        public DateTime Uploaded { get; set; }
        public string ImgSrc { get; set; }
        public Guid CommentId { get; set; }
        public string MemberId { get; set; }
        public Guid PostEntityModelId { get; set; }
        public virtual PostEntityModel Post { get; set; }

        public CommentEntityModel()
        {
            Id = Guid.NewGuid();
            //todo: date in i constructorn
        }
    }
}
