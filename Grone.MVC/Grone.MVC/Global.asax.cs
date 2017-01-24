using Grone.Data.IRepository;
using Grone.Data.Models;
using Grone.Data.Repository;
using Grone.MVC.App_Start;
using Grone.MVC.HelpClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Grone.MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        IPostRepository repo = new PostRepository();
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            var updatePostsThread = new Thread(RemoveOneFromEveryPost);
            updatePostsThread.Start();
        }

        private void RemoveOneFromEveryPost()
        {
            do
            {
                using (var context = new GroneEntities())
                {
                    foreach (var post in context.Posts)
                    {
                        if (post.TimeLeft > 0)
                            post.TimeLeft -= 1;
                        else
                        {
                            string filename = Path.GetFileName(post.ImgSrc);

                            FileHelper.RemoveFile(filename);

                            context.Posts.Remove(post);
                        }
                    }

                    context.SaveChanges();

                    Thread.Sleep(60000);
                }
            } while (true);
        }
    }
}
