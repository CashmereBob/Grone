using Grone.Data.IRepository;
using Grone.Data.Repository;
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
    }
}