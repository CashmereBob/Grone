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
using System.IO;

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
        public ActionResult Add(PostViewModel model, HttpPostedFileBase photoUpload)
        {

            if (Session["User"] == null)
            {
                Session["User"] = Guid.NewGuid();
            }

            //TODO : maximera filuppladdningen till 5mb
            string extension = Path.GetExtension(photoUpload.FileName); //Plockar ut filtypen (jpg, eller gif)
            string fileName = Guid.NewGuid().ToString() + extension; //Sätte ett nytt namn och lägger till filtypen
            string renamedPhotoPath = Server.MapPath("~/Img/" + fileName); //Sätter var filen ska läggas

            model.ImgSrc = $" /Img/{fileName}"; //Sparar sökvägen i modellen
            model.MemberId = Session["User"].ToString();

            photoUpload.SaveAs(renamedPhotoPath); //Sparar bilden i vald mapp
            repository.AddOrUpdate(PostViewToEntity.PostViewModelToEntity(model)); //Spara modellen i databasen
            return Content("sucsses");
        }

        [HttpGet]
        public ActionResult GetPreviewCommentsByPosts(Guid id)
        {
            List<CommentViewModel> viewModel = new List<CommentViewModel>();
            repository.GetTop3Comments(new PostEntityModel { Id = id }).ToList().ForEach(x => viewModel.Add(CommentViewToEntity.ToModelComment(x)));
            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }
        //TODO: Fixa så att jag kan få tag på alla comments via ett post. byta plats på top3comments och getcommentsbypost
        public ActionResult GetCommentsBypost(Guid id)
        {
            List<CommentViewModel> viewModel = new List<CommentViewModel>();
            commentRepository.GetByPostId(id).ToList().ForEach(x => viewModel.Add(CommentViewToEntity.ToModelComment(x)));
            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }
    }
}