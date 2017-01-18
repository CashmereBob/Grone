using Grone.Data.Models;
using Grone.Data.Repository;
using Grone.MVC.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Grone.MVC.HelpClasses
{
    public static class PostViewToEntity
    {
        public static PostViewModel ToModelPost(PostEntityModel entity)
        {
            var model = new PostViewModel()
            {
                Id = entity.Id,
                Date = entity.Uploaded,
                Description = entity.Description,
                ImgSrc = entity.ImgSrc,
                MemberId = entity.MemberId,
                TimeAdded = entity.TimeAdded,
                TimeLeft = entity.TimeLeft,
                Title = entity.Title,
            };

            return model;
        }
        public static PostEntityModel ToEntityPost(PostViewModel model)
        {
            var entity = new PostEntityModel()
            {
                Id = model.Id,
                Description = model.Description,
                ImgSrc = model.ImgSrc,
                TimeAdded = model.TimeAdded,
                MemberId = model.MemberId,
                TimeLeft = model.TimeLeft,
                Title = model.Title,
                TotalAdded = model.TimeAdded,
                Uploaded = model.Date
            };
            return entity;
        }
    }
}