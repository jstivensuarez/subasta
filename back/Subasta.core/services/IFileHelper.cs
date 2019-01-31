using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Subasta.core.services
{
    public interface IFileHelper
    {
        void DownLoadFile(string folder, IFormFile file, string name);

        void RemoveFile(string folder, string name);
    }
}
