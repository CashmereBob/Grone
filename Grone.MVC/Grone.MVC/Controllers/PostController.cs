using Grone.Data.Repository;
using Grone.Data.IRepository;
using Grone.Data.Models;
using Grone.MVC.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
                repository.GetAll().ToList().ForEach(x => viewModel.Add(new PostViewModel {
                    Id = x.Id,
                    Date = x.Uploaded,
                    Title = x.Title,
                    ImgSrc = x.ImgSrc,
                    Description = x.Description,
                    MemberId = x.MemberId,
                    TimeAdded = x.TimeAdded,
                    TimeLeft = x.TimeLeft
                }));
                return Json(viewModel);
            }
            else
            {
                ModelState.AddModelError("Error", "Something went wrong, reload the page.");
                return View();
            }
        }
    }
}