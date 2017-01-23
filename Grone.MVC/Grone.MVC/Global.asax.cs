using Grone.Data.IRepository;
using Grone.Data.Models;
using Grone.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Grone.MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        //IPostRepository repo = new PostRepository();
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var updatePostsThread = new Thread(RemoveOneFromEveryPost);

            updatePostsThread.Start();
        }

        public void RemoveOneFromEveryPost()
        {
            do
            {
                using (var context = new GroneEntities())
                {
                    foreach (var post in context.Posts)
                    {
                        post.TimeLeft -= 1;
                    }

                    context.SaveChanges();

                    Thread.Sleep(60000);
                }
            } while (true);
        }
    }
}
