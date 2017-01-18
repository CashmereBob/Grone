using Grone.Data.IRepository;
using Grone.Data.Repository;
using Grone.MVC.HelpClasses;
using Grone.MVC.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Grone.MVC.Controllers
{
    public class CommentController : Controller
    {
        public ICommentRepository repository;
        public CommentController()
        {
            repository = new CommentRepository();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddComment(CommentViewModel model)
        {
            if (model != null)
            {
                repository.Add(CommentViewToEntity.ToEntityComment(model));
                return Json(model);
            }
            else
            {
                ModelState.AddModelError("Error", "Comment not valid");
                return View("Index");
            }
            
        }
    }
}