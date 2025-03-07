using Apps.LanguageCloud.Dtos;
using Blackbird.Applications.Sdk.Common;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;

namespace Apps.LanguageCloud.Models.Files.Responses
{
    public class GetTargetFileInfoResponse
    {
        [Display("File ID")]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }

        [Display("Language direction")]
        public LanguageDirection? languageDirection { get; set; }

        [Display("Source file")]
        public SourceFile? sourceFile { get; set; }

        [Display("Latest version")]
        public VersionDto LatestVersion { get; set; }

        [Display("Analysis statistics")]
        public analysis? Analysis { get; set; }

        public GetTargetFileInfoResponse(TargetFileInfoDto info) 
        {
            Id = info.id;
            Name = info.Name;
            Status = info.status;
            languageDirection = info.languageDirection;
            sourceFile = info.sourceFile;
            LatestVersion = info.LatestVersion;
            Analysis = info.analysisStatistics == null ? null : new analysis
            {
                exactMatch = info.analysisStatistics?.exactMatch?.words ?? 0,
                inContextExactMatch = info.analysisStatistics?.inContextExactMatch?.words ?? 0,
                perfectMatch = info.analysisStatistics?.perfectMatch?.words ?? 0,
                newWords = info.analysisStatistics?.New?.words ?? 0,
                repetitions = info.analysisStatistics?.repetitions?.words ?? 0,
                crossDocumentRepetitions = info.analysisStatistics?.crossDocumentRepetitions?.words ?? 0,
                machineTranslation = info.analysisStatistics?.machineTranslation?.words ?? 0,
                total = info.analysisStatistics?.total?.words ?? 0,
                Fuzzy_50_74 = info.analysisStatistics?.fuzzyMatch?.FirstOrDefault(x => x.category?.minimum == 50)?.count?.words ?? 0,
                Fuzzy_75_84 = info.analysisStatistics?.fuzzyMatch?.FirstOrDefault(x => x.category?.minimum == 75)?.count?.words ?? 0,
                Fuzzy_85_94 = info.analysisStatistics?.fuzzyMatch?.FirstOrDefault(x => x.category?.minimum == 85)?.count?.words ?? 0,
                Fuzzy_95_99 = info.analysisStatistics?.fuzzyMatch?.FirstOrDefault(x => x.category?.minimum == 95)?.count?.words ?? 0
            };
        }

    }

    public class analysis 
    {
        [Display("Exact matches")]
        public int? exactMatch { get; set; }

        [Display("In-context exact matches")]
        public int? inContextExactMatch { get; set; }

        [Display("Perfect matches")]
        public int? perfectMatch { get; set; }

        [Display("New words")]
        public int? newWords { get; set; }

        [Display("Repetitions")]
        public int? repetitions { get; set; }

        [Display("Cross-document repetitions")]
        public int? crossDocumentRepetitions { get; set; }

        [Display("Machine Translation")]
        public int? machineTranslation { get; set; }

        [Display("Fuzzy 50 - 74")]
        public int? Fuzzy_50_74 { get; set; }

        [Display("Fuzzy 75 - 84")]
        public int? Fuzzy_75_84 { get; set; }

        [Display("Fuzzy 85 - 94")]
        public int? Fuzzy_85_94 { get; set; }

        [Display("Fuzzy 95 - 99")]
        public int? Fuzzy_95_99 { get; set; }

        [Display("Total")]
        public int? total { get; set; }
    }
}
