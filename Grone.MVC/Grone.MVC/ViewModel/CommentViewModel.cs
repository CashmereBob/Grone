using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Grone.MVC.ViewModel
{
    public class CommentViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Can't leave this blank")]
        [MaxLength(500 ,ErrorMessage = "Must be between 1-500 characters")]
        public string Comment  { get; set; }
        public string Date { get; set; }
        public Guid CommentId { get; set; }
        public Guid PostId { get; set; }
        public string MemberId { get; set; }
        public string ImgSrc { get; set; }
    }
}