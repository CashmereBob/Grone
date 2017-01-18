using Grone.Data.Repository;
using Grone.Data.IRepository;
using Grone.Data.Models;
using Grone.MVC.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Grone.MVC.HelpClasses;

namespace Grone.MVC.Controllers
{
    public class PostController: Controller
    {
        public IPostRepository repository;
        public ICommentRepository commentRepository;
        public PostController()
        {
            repository = new PostRepository();
            commentRepository = new CommentRepository();
        }

        [HttpGet]
        public ActionResult GetAllPosts()
        {
                List<PostViewModel> viewModel = new List<PostViewModel>();
                repository.GetAll().ToList().ForEach(x => viewModel.Add(PostViewToEntity.PostEntityViewModelToModel(x)));
                return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Add(PostViewModel model)
        {
            repository.AddOrUpdate(PostViewToEntity.PostViewModelToEntity(model));
            return Content("sucsses");
        }

        [HttpGet]
        public ActionResult ViewCommentsByPosts(Guid id)
        {
            List<CommentViewModel> viewModel = new List<CommentViewModel>();
            repository.GetTop3Comments(new PostEntityModel { Id = id }).ToList().ForEach(x => viewModel.Add(CommentViewToEntity.ToModelComment(x)));
            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }
    }
}