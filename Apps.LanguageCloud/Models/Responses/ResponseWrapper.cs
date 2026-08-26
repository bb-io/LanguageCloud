namespace Apps.LanguageCloud.Models.Responses;

public class ResponseWrapper<T>
{
    public T Items { get; set; }

    public int? ItemCount { get; set; }
}
