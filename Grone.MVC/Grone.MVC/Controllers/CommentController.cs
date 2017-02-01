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
    [AllowAnonymous]
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
            try
            {
                ValidationHandler val = new ValidationHandler();
                if (val.ValidateCommentViewModel(model))
                {
                    if (model != null)
                    {
                        if (model.Comment.Count() <= 500)
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
                                //TODO : maximera filuppladdningen till 5mb *****
                                string extension = Path.GetExtension(photoUpload.FileName); //Plockar ut filtypen (jpg, eller gif)
                                string fileName = Guid.NewGuid().ToString() + extension; //Sätte ett nytt namn och lägger till filtypen
                                string renamedPhotoPath = Server.MapPath("~/Img/" + fileName); //Sätter var filen ska läggas

                                model.ImgSrc = $" /Img/{fileName}"; //Sparar sökvägen i modellen
                                photoUpload.SaveAs(renamedPhotoPath); //Sparar bilden i vald mapp
                            }

                            model.MemberId = Session["User"].ToString();
                            var entity = EFMapper.ModelToEntity(model);
                            repository.Add(entity);
                            return Json(entity);
                        }
                        else
                        {
                            return Content("Commentmodel problem");
                        }
                    }
                }
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (InvalidOperationException e)
            {
                return Content("Problem with comment and InvalidOperationException: " + e.Message);
            }
            catch (Exception e)
            {
                return Content("Problem with comment: " + e.Message);
            }
        }
    }
}