using DocumentFormat.OpenXml.Vml;
using Newtonsoft.Json;

namespace Apps.LanguageCloud.Dtos;

public class TargetFileInfoDto
{
    public string id { get; set; }
    public string Name { get; set; }
    public LanguageDirection? languageDirection { get; set; }
    public SourceFile? sourceFile { get; set; }
    public AnalysisStatistics? analysisStatistics { get; set; }
    public string status { get; set; }

    [JsonProperty("latestVersion")]
    public VersionDto LatestVersion { get; set; }
}

public class AnalysisStatistics
{
    public ExactMatch exactMatch { get; set; }
    public InContextExactMatch inContextExactMatch { get; set; }
    public PerfectMatch perfectMatch { get; set; }
    public New New { get; set; }
    public Repetitions repetitions { get; set; }
    public CrossDocumentRepetitions crossDocumentRepetitions { get; set; }
    public MachineTranslation machineTranslation { get; set; }
    public List<FuzzyMatch> fuzzyMatch { get; set; }
    public Total total { get; set; }
    }

    public class Category
{
    public int minimum { get; set; }
    public int maximum { get; set; }
}

public class Count
{
    public int words { get; set; }
}

public class CrossDocumentRepetitions
{
    public int words { get; set; }
}

public class ExactMatch
{
    public int words { get; set; }
}

public class FuzzyMatch
{
    public Count count { get; set; }
    public Category category { get; set; }
}

public class InContextExactMatch
{
    public int words { get; set; }
}

public class MachineTranslation
{
    public int words { get; set; }
}

public class New
{
    public int words { get; set; }
}

public class PerfectMatch
{
    public int words { get; set; }
}

public class Repetitions
{
    public int words { get; set; }
}

public class Total
{
    public int words { get; set; }
}