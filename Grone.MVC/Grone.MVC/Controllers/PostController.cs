﻿using Grone.Data.Repository;
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
        public ActionResult GetAllPosts()
        {
            if (ModelState.IsValid)
            {
                repository.GetAll().ToList();
                return View();
            }
            else
            {
                ModelState.AddModelError("Error", "Something went wrong, reload the page.");
                return View();
            }
        }
    }
}