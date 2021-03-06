﻿using Grone.Data.IRepository;
using Grone.Data.Models;
using Grone.Data.Repository;
using Grone.MVC.HelpClasses;
using Grone.MVC.ViewModel;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Grone.MVC.Controllers
{
    public class AuthController : Controller
    {
        public IUserRepository repo;
        public IPostRepository postRepo;
        public ICommentRepository commentRepo;

        public AuthController()
        {
            repo = new UserRepository();
            postRepo = new PostRepository();
            commentRepo = new CommentRepository();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            List<UserEntityModel> usersFromDb = repo.GetAll();

            if (usersFromDb.Count == 0 || usersFromDb == null)
                return RedirectToAction("Add");
            else
                return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index(UserLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserEntityModel userToLogin = repo.CheckCredentials(model.eMail, model.Password);

                if (userToLogin != null)
                {
                    // when user is found, set the necessary userloginviewmodel properties
                    model.Id = userToLogin.Id;
                    model.Fullname = userToLogin.Fullname;
                    model.eMail = userToLogin.eMail;
                    model.Password = userToLogin.Password;

                    var identity = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, model.Fullname),
                        new Claim(ClaimTypes.Email, model.eMail),
                        new Claim(ClaimTypes.NameIdentifier, model.Id.ToString()),
                    }, "ApplicationCookie");

                    IOwinContext owinCtx = HttpContext.GetOwinContext();
                    IAuthenticationManager authManager = owinCtx.Authentication;
                    authManager.SignIn(identity);

                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Add()
        {
            if (User.Identity.IsAuthenticated == false && repo.GetAll().Count > 0)
            {
                return RedirectToAction("Index");
            }
            else
                return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddUserViewModel model)
        {
            if (User.Identity.IsAuthenticated == false && repo.GetAll().Count > 0)
            {
                return Content("Fail");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    if (repo.EmailAlreadyExists(model.Email))
                        return View(model);

                    else
                    {
                        var entity = EFMapper.ModelToEntity(model);

                        repo.Add(entity);

                        return Content("User Added");
                    }
                }
                return Content("Fail");
            }
        }

        [HttpGet]
        [Authorize]
        public ActionResult Update()
        {
            //get current user id
            ClaimsIdentity currentIdentity = User.Identity as ClaimsIdentity;
            string mail = currentIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
            var user = repo.GetUserByMail(mail);

            UpdateUserViewModel model = EFMapper.EntityToModel(user);
            model.Password = "";
            model.RePassword = "";

            return PartialView("_ManageAccount", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Update(UpdateUserViewModel model)
        {
            //get current user id
            var user = repo.GetUserByMail(model.Email);
            //create entity and populate with updated info
            var entity = new UserEntityModel();

            entity.Id = user.Id;
            entity.eMail = model.Email;
            entity.Fullname = model.FullName;

            if (!string.IsNullOrWhiteSpace(model.Password))
            { entity.Password = model.Password; }


            repo.Update(entity);

            return Content(model.FullName);

        }

        [Authorize]
        public ActionResult Logout()
        {
            if (User.Identity.IsAuthenticated == true)
            {
                var ctx = Request.GetOwinContext();
                var authManager = ctx.Authentication;
                authManager.SignOut("ApplicationCookie");
                return RedirectToAction("index", "home");
            }
            else
                return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult DeletePost(PostViewModel model)
        {

            var entity = postRepo.GetById(model.Id);

            //if post has an image, remove it
            if (!string.IsNullOrWhiteSpace(entity.ImgSrc))
            {
                string postImg = Path.GetFileName(entity.ImgSrc);
                FileHelper.RemoveFile(postImg);
            }

            //if comments belonging to post has images, remove them
            var postComments = commentRepo.GetByPostId(model.Id);
            foreach (var comment in postComments)
            {
                if (!string.IsNullOrWhiteSpace(comment.ImgSrc))
                {
                    //if current comment has an image, remove it
                    string commentImg = Path.GetFileName(comment.ImgSrc);
                    FileHelper.RemoveFile(commentImg);
                }
            }

            // remove the post
            postRepo.Delete(model.Id);

            return Content("Post Deleted Successfull");
        }
        [Authorize]
        public ActionResult DeleteComment(CommentViewModel model)
        {
            var entity = commentRepo.GetById(model.Id);

            entity.Comment = "Comment has been removed due to Anon breaking the Grone guidelines for accepted mannerism";

            if (!string.IsNullOrWhiteSpace(entity.ImgSrc)) // if comment has an image
            {
                //remove the image for the comment
                string commentImg = Path.GetFileName(entity.ImgSrc);

                FileHelper.RemoveFile(commentImg);
            }
            entity.ImgSrc = "/Content/forbidden.png";

            commentRepo.Update(entity);

            return Content("Comment Deleted Successfull");
        }
    }
}