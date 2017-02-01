using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Grone.Data.Models;
using Grone.MVC.ViewModel;

namespace Grone.MVC.HelpClasses
{
    public static class EFMapper
    {
        internal static UserEntityModel ModelToEntity(AddUserViewModel model)
        {
            var entity = new UserEntityModel();

            entity.eMail = model.Email;
            entity.Fullname = model.FullName;
            entity.Password = model.Password;

            return entity;
        }

        internal static UpdateUserViewModel EntityToModel(UserEntityModel entity)
        {
            UpdateUserViewModel model = new UpdateUserViewModel()
            {
                Email = entity.eMail,
                FullName = entity.Fullname,
                Password = entity.Password,
            };

            return model;
        }

        internal static CommentViewModel EntityToModel(CommentEntityModel entity)
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

        internal static CommentEntityModel ModelToEntity(CommentViewModel model)
        {
            if (model.Id == Guid.Empty)
            {
                var entity = new CommentEntityModel()
                {
                    CommentId = model.CommentId,
                    PostEntityModelId = model.PostId,
                    MemberId = model.MemberId,
                    ImgSrc = model.ImgSrc,
                    Comment = model.Comment
                };
                return entity;
            }
            else
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