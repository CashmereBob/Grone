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
                Date = entity.Uploaded,
                Id = entity.Id,
                PostId = entity.PostEntityModelId
            };
            return model;
           
        }
        public static CommentEntityModel ToEntityComment(CommentViewModel model)
        {
            var entity = new CommentEntityModel()
            {
                CommentId = model.CommentId,
                PostEntityModelId = model.PostId,
                MemberId = model.MemberId,
                ImgSrc = model.ImgSrc,
                Uploaded = model.Date,
                Id = model.Id
            };
            return entity;
        }
    }
}