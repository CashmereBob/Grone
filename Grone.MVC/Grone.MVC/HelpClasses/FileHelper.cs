using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Grone.MVC.HelpClasses
{
    public static class FileHelper
    {
        public static void RemoveFile(string filename)
        {
            string filepath = "~/Img/" + filename;

            File.Delete(System.Web.Hosting.HostingEnvironment.MapPath(filepath));
        }
    }
}