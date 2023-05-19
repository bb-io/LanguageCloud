﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.LanguageCloud.Dtos
{
    public class TranslationMemoryDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<LanguageDirectionItem> LanguageDirections { get; set; }
    }

    public class LanguageDirectionItem
    {
        public LanguageDirectionObj LanguageDirection { get; set; }
    }

    public class LanguageDirectionObj
    {
        public LanguageObj SourceLanguage { get; set; }
        public LanguageObj TargetLanguage { get; set; }
    }
    public class LanguageObj
    {
        public string LanguageCode { get; set; }
        public string EnglishName { get; set; }
    }

}
