namespace Apps.LanguageCloud.Dtos;

public class UpdateFileImportDto
{
    public string Id { get; set; }
    public string Status { get; set; }
    public string errorMessage { get; set; }
    public string fileVersionId { get; set; }
}