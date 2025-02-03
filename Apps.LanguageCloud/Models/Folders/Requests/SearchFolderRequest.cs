using Blackbird.Applications.Sdk.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.LanguageCloud.Models.Folders.Requests;
public class SearchFolderRequest
{
    [Display("Folder name")]
    public string? FolderName { get; set; }
}
