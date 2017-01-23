using Grone.Data.IRepository;
using Grone.Data.Repository;
using Grone.MVC.HelpClasses;
using Grone.MVC.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
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
        public ActionResult AddComment(CommentViewModel model, HttpPostedFileBase photoUpload)
        {
            if (model != null)
            {
                //TODO : maximera filuppladdningen till 5mb
                string extension = Path.GetExtension(photoUpload.FileName); //Plockar ut filtypen (jpg, eller gif)
                string fileName = Guid.NewGuid().ToString() + extension; //Sätte ett nytt namn och lägger till filtypen
                string renamedPhotoPath = Server.MapPath("~/Img/" + fileName); //Sätter var filen ska läggas

                model.ImgSrc = $" /Img/{fileName}"; //Sparar sökvägen i modellen

                photoUpload.SaveAs(renamedPhotoPath); //Sparar bilden i vald mapp

                repository.Add(CommentViewToEntity.ToEntityComment(model));
                return Json(model);
            }
            else
            {
                ModelState.AddModelError("Error", "Comment not valid");
                return View("Index");
            }
            
        }
    }
}