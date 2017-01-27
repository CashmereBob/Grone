using Grone.Data.IRepository;
using Grone.Data.Models;
using Grone.Data.Repository;
using Grone.MVC.App_Start;
using Grone.MVC.HelpClasses;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Helpers;
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
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;

            var updatePostsThread = new Thread(RemoveOneFromEveryPost);
            updatePostsThread.Start();

            if (bool.Parse(ConfigurationManager.AppSettings["MigrateDatabaseToLatestVersion"]))
            {
                var configuration = new Grone.Data.Migrations.Configuration();
                var migrator = new DbMigrator(configuration);
                migrator.Update();
            }

        }

        private void RemoveOneFromEveryPost()
        {
            do
            {

                Thread.Sleep(60000);

                var allPosts = repo.GetAll().ToList();

                foreach (var post in allPosts)
                {
                    post.TimeLeft -= 1;
                    repo.AddOrUpdate(post);

                    if (post.TimeLeft == 0)
                    {

                        if (!string.IsNullOrWhiteSpace(post.ImgSrc))
                        {
                            string filename = Path.GetFileName(post.ImgSrc);

                            FileHelper.RemoveFile(filename);
                        }

                        var comments = repo.GetComments(post);

                        foreach (var comment in comments)
                        {
                            if (!string.IsNullOrWhiteSpace(comment.ImgSrc))
                            {
                                string commentImgFilename = Path.GetFileName(comment.ImgSrc);
                                FileHelper.RemoveFile(commentImgFilename);
                            }
                        }

                        repo.Delete(post.Id);

                    }
                }

            } while (true);

        }
    }
}
