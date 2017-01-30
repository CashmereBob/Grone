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
    }
}