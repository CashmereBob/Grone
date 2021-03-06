﻿using Grone.Data.Models;
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
        public static PostViewModel PostEntityViewModelToModel(PostEntityModel entity)
        {
            var model = new PostViewModel()
            {
                Id = entity.Id,
                Date = entity.Uploaded.ToString(),
                Description = entity.Description,
                ImgSrc = entity.ImgSrc,
                MemberId = entity.MemberId,
                TimeAdded = entity.TotalTimeAdded,
                TimeLeft = entity.TimeLeft,
                Title = entity.Title,
            };

            return model;
        }
        //Check if totaladded is removed or not.
        public static PostEntityModel PostViewModelToEntity(PostViewModel model)
        {
            var entity = new PostEntityModel()
            {
                Description = model.Description,
                ImgSrc = model.ImgSrc,
                TotalTimeAdded = model.TimeAdded,
                MemberId = model.MemberId,
                TimeLeft = model.TimeLeft,
                Title = model.Title
            };
            return entity;
        }
    }
}