﻿using Grone.Data.Repository;
using Grone.Data.IRepository;
using Grone.Data.Models;
using Grone.MVC.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Grone.MVC.HelpClasses;
using System.IO;

namespace Grone.MVC.Controllers
{
    [AllowAnonymous]
    public class PostController : Controller
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

            foreach (var post in viewModel)
            {
                repository.GetComments(new PostEntityModel(post.Id)).ToList().ForEach(x => post.Comments.Add(EFMapper.EntityToModel(x)));
            }

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Add(PostViewModel model, HttpPostedFileBase photoUpload)
        {
            ValidationHandler val = new ValidationHandler();
            if (val.ValidatePostViewModel(model))
            {
                try
                {
                    if (Session["User"] == null || Session["User"].ToString() == " **ADMIN**")
                    {
                        Session["User"] = Guid.NewGuid();
                    }

                    if (User.Identity.IsAuthenticated && Session["User"].ToString().Count() == 36)
                    {
                        Session["User"] = " **ADMIN**";
                    }

                    if (photoUpload != null)
                    {
                        //TODO : maximera filuppladdningen till 5mb

                        string extension = Path.GetExtension(photoUpload.FileName); //Plockar ut filtypen (jpg, eller gif)
                        string fileName = Guid.NewGuid().ToString() + extension; //Sätte ett nytt namn och lägger till filtypen
                        string renamedPhotoPath = Server.MapPath("~/Img/" + fileName); //Sätter var filen ska läggas

                        model.ImgSrc = $" /Img/{fileName}"; //Sparar sökvägen i modellen
                        photoUpload.SaveAs(renamedPhotoPath); //Sparar bilden i vald mapp
                    }

                    model.MemberId = Session["User"].ToString();
                    var entity = PostViewToEntity.PostViewModelToEntity(model);
                    repository.AddOrUpdate(entity); //Spara modellen i databasen
                    return Json(entity);
                }
                catch (Exception f)
                {
                    return Content("PostAddException", "Something went horribly wrong send the following information to an admin and/or reload the page: " + f.Message);
                }
                //return Json(model, JsonRequestBehavior.AllowGet);
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetPreviewCommentsByPosts(Guid id)
        {
            List<CommentViewModel> viewModel = new List<CommentViewModel>();
            repository.GetTop3Comments(new PostEntityModel { Id = id }).ToList().ForEach(x => viewModel.Add(EFMapper.EntityToModel(x)));
            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCommentsBypost(Guid id)
        {
            List<CommentViewModel> viewModel = new List<CommentViewModel>();
            repository.GetComments(new PostEntityModel { Id = id }).ToList().ForEach(x => viewModel.Add(EFMapper.EntityToModel(x)));
            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }
    }
}