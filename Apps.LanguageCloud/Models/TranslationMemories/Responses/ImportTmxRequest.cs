﻿using Blackbird.Applications.Sdk.Common;
using File = Blackbird.Applications.Sdk.Common.Files.File;

namespace Apps.LanguageCloud.Models.TranslationMemories.Responses;

public class ImportTmxRequest
{
    [Display("Translation memory ID")]
    public string TranslationMemoryId { get; set; }

    [Display("File")]
    public File File { get; set; }

    public string SourceLanguage { get; set; }

    public string TargetLanguage { get; set; }
}