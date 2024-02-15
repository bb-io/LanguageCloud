using Apps.LanguageCloud.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Newtonsoft.Json;

namespace Apps.LanguageCloud.Models.Projects.Requests;

public class CreateFromTemplateRequest
{
    public string Name { get; set; }


    [DataSource(typeof(ProjectTemplateDataHandler))]
    public string Template { get; set; }

    public string DueBy { get; set; }

    [DataSource(typeof(LocationDataHandler))]
    public string Location { get; set; }

    [DataSource(typeof(LanguageDataHandler))]
    public string SourceLanguage { get; set; }

    public List<string> TargetLanguages { get; set; }

   [DataSource(typeof(TranslationEngineDataHandler))]
    public string? TranslationEngine { get; set; }

    public string? FileProcessingConfiguration { get; set; }

    public string? Workflow { get; set; }




    public string GetSerializedRequest()
    {
        return JsonConvert.SerializeObject(new
        {
            name = Name,
            dueBy = DueBy,
            projectTemplate = new { id = Template },
            location = Location,
            translationEngine = new { id = TranslationEngine },
            fileProcessingConfiguration = new { id = FileProcessingConfiguration },
            workflow = new { id = Workflow },
            languageDirections = TargetLanguages.Select(t => new
            {
                sourceLanguage = new { languageCode = SourceLanguage },
                targetLanguage = new { languageCode = t }
            }).ToArray()} 
        , new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
    }
}