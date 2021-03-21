using System;
using System.IO;
using System.Linq;
using System.Web;

namespace Core.Common
{
    public class FileUtilCommon
    {
        private static readonly byte[] DOC = { 208, 207, 17, 224, 161, 177, 26, 225 };
        private static readonly byte[] JPG = { 255, 216, 255 };
        private static readonly byte[] PDF = { 37, 80, 68, 70, 45, 49, 46 };
        private static readonly byte[] PNG = { 137, 80, 78, 71, 13, 10, 26, 10, 0, 0, 0, 13, 73, 72, 68, 82 };
        
        public class MemoryPostedFile : HttpPostedFileBase
        {
            private readonly byte[] fileBytes;

            public MemoryPostedFile(byte[] fileBytes, string fileName = null, string contenType = null)
            {
                this.fileBytes = fileBytes;
                this.FileName = fileName;
                this.InputStream = new MemoryStream(fileBytes);
                this.ContentType = contenType;
            }

            public override int ContentLength => fileBytes.Length;

            public override string FileName { get; }

            public override Stream InputStream { get; }

            public override string ContentType { get; }
        }
    }
}
