using Grone.Data.Models;
using Grone.Data.Repository;
using Grone.MVC.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Grone.MVC.HelpClasses
{
    public static class CommentViewToEntity
    {
        public static CommentViewModel ToModelComment(CommentEntityModel entity)
        {
            var model = new CommentViewModel()
            {
                CommentId = entity.CommentId,
                MemberId = entity.MemberId,
                ImgSrc = entity.ImgSrc,
                Date = entity.Uploaded.ToString(),
                Id = entity.Id,
                PostId = entity.PostEntityModelId,
                Comment = entity.Comment
            };
            return model;
           
        }
        public static CommentEntityModel ToEntityComment(CommentViewModel model)
        {
            if (model.Id == Guid.Empty) { 
            var entity = new CommentEntityModel()
            {
                CommentId = model.CommentId,
                PostEntityModelId = model.PostId,
                MemberId = model.MemberId,
                ImgSrc = model.ImgSrc,
                Comment = model.Comment
            };
                return entity;
            } else
            {
                var entity = new CommentEntityModel(model.Id)
                {
                    CommentId = model.CommentId,
                    PostEntityModelId = model.PostId,
                    MemberId = model.MemberId,
                    ImgSrc = model.ImgSrc,
                    Comment = model.Comment
                };
                return entity;


            }

            
        }
    }
}