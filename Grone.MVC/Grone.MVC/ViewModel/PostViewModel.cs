using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Grone.MVC.ViewModel
{
    public class PostViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "You have to type in a title")]
        [MaxLength (30, ErrorMessage = "The lengt must be between 1-30 characters")]
        public string Title { get; set; }
        [Required(ErrorMessage = "You have to type in a description")]
        [MaxLength (500, ErrorMessage = "The lengt must be between 1-500 characters")]

        public string Description { get; set; }
        [Display(Name ="Image")]
        public string ImgSrc { get; set; }
        public string Date { get; set; }
        public int TimeLeft{ get; set; }
        public int TimeAdded { get; set; }
        public string MemberId { get; set; }
        public List<CommentViewModel> Comments { get; set; }

        public PostViewModel()
        {
            Comments = new List<CommentViewModel>();
        }
    }
}