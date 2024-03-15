using Apps.LanguageCloud.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Newtonsoft.Json;
using Blackbird.Applications.Sdk.Common;

namespace Apps.LanguageCloud.Models.Projects.Requests;

public class CreateFromTemplateRequest
{
    public string Name { get; set; }


    [DataSource(typeof(ProjectTemplateDataHandler))]
    public string Template { get; set; }

    [Display("Due By")]
    public DateTime DueBy { get; set; }

    [DataSource(typeof(LocationDataHandler))]
    public string Location { get; set; }

    [DataSource(typeof(LanguageDataHandler))]

    [Display("Source Language")]
    public string SourceLanguage { get; set; }

    [Display("Target Languages")]
    public List<string> TargetLanguages { get; set; }

   [DataSource(typeof(TranslationEngineDataHandler))]
    [Display("Translation Engine")]
    public string TranslationEngine { get; set; }

    [DataSource(typeof(FileProcessingConfigurationDataHandler))]
    [Display("File Processing Configuration")]
    public string FileProcessingConfiguration { get; set; }

    [DataSource(typeof(WorkflowDataHandler))]
    
    public string Workflow { get; set; }




    public string GetSerializedRequest()
    {
        return JsonConvert.SerializeObject(new
        {
            name = Name,
            dueBy = DueBy.ToString("yyyy-MM-ddTHH:mm:ss.000Z"),
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