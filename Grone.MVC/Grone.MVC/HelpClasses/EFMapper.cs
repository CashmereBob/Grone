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
        internal static UserEntityModel ModelToEntity(UserAddViewModel model)
        {

            var entity = new UserEntityModel()
            {
                entity.eMail = model.eMail,
                entity.Fullname = model.Fullname,
                entity.Password = model.Password,
                // todo ivan: salt
            };
            return entity;
        }
    }
}