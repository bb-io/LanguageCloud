using Apps.LanguageCloud.DataSourceHandlers;
using Apps.LanguageCloud.DataSourceHandlers.EnumHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Newtonsoft.Json;

namespace Apps.LanguageCloud.Models.Projects.Requests;

public class CreateProjectRequest
{
    public string Name { get; set; }

    public string? Description { get; set; }

    [Display("Due by")]
    public DateTime DueBy { get; set; }

    [Display("Source language")]
    [DataSource(typeof(LanguageDataHandler))]
    public string SourceLanguage { get; set; }

    [Display("Target languages")]
    [DataSource(typeof(LanguageDataHandler))]
    public List<string> TargetLanguages { get; set; }

    [Display("Folder Location")]
    [DataSource(typeof(LocationDataHandler))]
    public string Location { get; set; }
    

    [Display("Translation engine")]
    [DataSource(typeof(TranslationEngineDataHandler))]
    public string? TranslationEngine { get; set; }
   
    [Display("Translation engine strategy")]
    [DataSource(typeof(StrategyDataHandler))]
    public string? TranslationEngineStrategy { get; set; }

    [DataSource(typeof(FileProcessingConfigurationDataHandler))]
    [Display("File processing configuration")]
    public string FileProcessingConfiguration { get; set; }

    [Display("File processing configuration strategy")]
    [DataSource(typeof(StrategyDataHandler))]
    public string? FileProcessingConfigurationStrategy { get; set; }

    [DataSource(typeof(WorkflowDataHandler))]
    [Display("Workflow")]
    public string Workflow { get; set; }

    [Display("Workflow strategy")]
    [DataSource(typeof(StrategyDataHandler))]
    public string? WorkflowStrategy { get; set; }

    [Display("Pricing model")]
    public string? PricingModel { get; set; }

    [Display("Pricing model strategy")]
    [DataSource(typeof(StrategyDataHandler))]
    public string? PricingModelStrategy { get; set; }

    [Display("TQA profile")]
    public string? TQAProfile { get; set; }

    [Display("TQA profile strategy")]
    [DataSource(typeof(StrategyDataHandler))]
    public string? TQAProfileStrategy { get; set; }

    [Display("Force online")]
    public bool? ForceOnline { get; set; }

    [Display("Schedule template")]
    public string? ScheduleTemplate { get; set; }

    [Display("Schedule template strategy")]
    [DataSource(typeof(StrategyDataHandler))]
    public string? ScheduleTemplateStrategy { get; set; }

    public string GetSerializedRequest()
    {
        return JsonConvert.SerializeObject(new
        {
            name = Name,
            description = Description,
            dueBy = DueBy.ToString("yyyy-MM-ddTHH:mm:ss.000Z"),
            languageDirections = TargetLanguages.Select(t => new
            {
                sourceLanguage = new { languageCode = SourceLanguage },
                targetLanguage = new { languageCode = t }
            }).ToArray(),
            location = Location,
            translationEngine = TranslationEngine != null ?
            new
            {
                id = TranslationEngine,
                strategy = TranslationEngineStrategy
            } : null,
            fileProcessingConfiguration = FileProcessingConfiguration != null ?
            new
            {
                id = FileProcessingConfiguration,
                strategy = FileProcessingConfigurationStrategy
            } : null,
            workflow = Workflow != null ?
            new
            {
                id = Workflow,
                strategy = WorkflowStrategy
            } : null,
            pricingModel = PricingModel != null ? new
            {
                id = PricingModel,
                strategy = PricingModelStrategy
            } : null,
            tqaProfile = TQAProfile != null ? new
            {
                id = TQAProfile,
                strategy = TQAProfileStrategy
            } : null,
            forceOnline = ForceOnline,
            scheduleTemplate = ScheduleTemplate != null ? new
            {
                id = ScheduleTemplate,
                strategy = ScheduleTemplateStrategy
            } : null,
        }, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
    }
}