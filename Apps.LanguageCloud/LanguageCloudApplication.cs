using Blackbird.Applications.Sdk.Common;


namespace Apps.LanguageCloud;

public class LanguageCloudApplication : IApplication
{
    private string _name;
    private readonly Dictionary<Type, object> _typesInstances;

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