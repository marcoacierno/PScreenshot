using System;
using System.Collections.Generic;
using System.Text;

namespace Picu3
{
    struct SpecialPaths
    {
        public static string DocFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Picu\\";
        public static string UploadQueueFile = DocFolder + "upload_queue.txt";
    }
}
