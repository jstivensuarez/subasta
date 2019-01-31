using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Subasta.core.services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Text;

namespace Subasta.core.helpers
{
    public class FileHelper: IFileHelper
    {
        private IHostingEnvironment hostingEnvironment;

        public FileHelper(IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }

        public void DownLoadFile(string folder, IFormFile file, string name)
        {
            try
            {
                string webRootPath = hostingEnvironment.WebRootPath;
                string newPath = Path.Combine(webRootPath, folder);
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                if (file.Length > 0)
                {
                    string fullPath = Path.Combine(newPath,name);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void RemoveFile(string folder, string name)
        {
            try
            {
                string webRootPath = hostingEnvironment.WebRootPath;
                string newPath = Path.Combine(webRootPath, folder, name);
                if (!Directory.Exists(newPath))
                {
                    File.Delete(newPath);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
