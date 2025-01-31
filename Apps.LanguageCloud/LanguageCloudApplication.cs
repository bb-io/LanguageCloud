using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Metadata;


namespace Apps.LanguageCloud;

public class LanguageCloudApplication : IApplication, ICategoryProvider
{

    public IEnumerable<ApplicationCategory> Categories
    {
        get => [ApplicationCategory.CatAndTms];
        set { }
    }    

    public T GetInstance<T>()
    {
        throw new NotImplementedException();
    }

}