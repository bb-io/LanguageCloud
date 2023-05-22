﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.LanguageCloud.Models.Files.Requests
{
    public class UploadFileRequest
    {
        public string ProjectId { get; set; }

        public byte[] File { get; set; }

        public string FileName { get; set; }

        public string SourceLanguageCode { get; set; }
    }
}