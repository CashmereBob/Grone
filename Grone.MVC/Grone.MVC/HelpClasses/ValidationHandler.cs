using Grone.MVC.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Grone.MVC.HelpClasses
{
    public class ValidationHandler
    {
        public bool ValidateCommentViewModel(CommentViewModel model)
        {
            try
            {
                if (model.Id == Guid.Empty && model.Id != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                //return Content("Problem with Validate Comment" + e.Message);
            }
            return true;
        }
        public bool ValidatePostViewModel(PostViewModel model)
        {
            try
            {
                if (model.Id == Guid.Empty && model.Id != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
            }
            return true;
        }
    }
}