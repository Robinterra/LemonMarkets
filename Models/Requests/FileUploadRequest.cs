using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WsApiCore
{
    public class FileUploadRequest
    {

        #region get/set

        public FileInfo File
        {
            get;
        }

        public string FileName
        {
            get;
        }

        public string Name
        {
            get;
        }

        #endregion get/set

        #region ctor

        public FileUploadRequest(FileInfo file, string filename, string name)
        {
            this.File = file;
            this.FileName = filename;
            this.Name = name;
        }

        #endregion ctor

    }

}
