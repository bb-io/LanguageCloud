using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Metadata;


namespace Apps.LanguageCloud;

public class LanguageCloudApplication : IApplication, ICategoryProvider
{
    private string _name;
    private readonly Dictionary<Type, object> _typesInstances;

    public IEnumerable<ApplicationCategory> Categories
    {
        get => [ApplicationCategory.CatAndTms];
        set { }
    }
    
    public LanguageCloudApplication()
    {
        _name = "Language cloud";
    }

    public string Name
    {
        get => _name;
        set => _name = value;
    }

    public T GetInstance<T>()
    {
        throw new NotImplementedException();
    }

}