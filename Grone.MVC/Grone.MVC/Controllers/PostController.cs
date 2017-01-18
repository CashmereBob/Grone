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
    class PostController: Controller
    {
        public IPostRepository repository;
        public PostController()
        {
            repository = new PostRepository();
        }
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetAllPosts()
        {
            if (ModelState.IsValid)
            {
                List<PostViewModel> viewModel = new List<PostViewModel>();
                repository.GetAll().ToList().ForEach(x => viewModel.Add(PostViewToEntity.PostEntityViewModelToModel(x)));
                return Json(viewModel);
            }
            else
            {
                ModelState.AddModelError("Error", "Something went wrong, reload the page.");
                return View();
            }
        }
        [HttpGet]
        public ActionResult Add()
        {
            return PartialView("_AddPost");
        }
        public ActionResult Add(PostViewModel model)
        {
            repository.AddOrUpdate(PostViewToEntity.PostViewModelToEntity(model));
            return Json(model);
        }
        public ActionResult ViewCommentsByPosts(Guid id)
        {
            return View();
        }
    }
}