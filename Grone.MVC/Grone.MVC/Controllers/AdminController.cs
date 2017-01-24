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
    public class AdminController : Controller
    {
        public IUserRepository repo;
        public AdminController()
        {
            repo = new UserRepository();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        private bool SuccessfullLogin(UserEntityModel userToBeLoggedIn)
        {
            using (var context = new GroneEntities())
            {
                foreach (var user in context.Users)
                {
                    if (user.eMail == userToBeLoggedIn.eMail && user.Password == userToBeLoggedIn.Password)
                        return true;
                }
                return false;
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index(UserLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserEntityModel userToLogin = repo.CheckCredentials(model.eMail,model.Password);

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

        public ActionResult Logout()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;
            authManager.SignOut("ApplicationCookie");
            return RedirectToAction("index", "home");
        }
    }
}