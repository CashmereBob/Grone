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
        GroanEntities _dbcontext = new GroanEntities();
        public List<Post> Posts = new List<Post>();
        public ActionResult Index()
        {
            return View();
        }
    }
}