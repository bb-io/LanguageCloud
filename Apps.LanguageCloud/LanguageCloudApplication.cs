using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication.OAuth2;


namespace Apps.LanguageCloud
{
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
            if (!_typesInstances.TryGetValue(typeof(T), out var value))
            {
                throw new InvalidOperationException($"Instance of type '{typeof(T)}' not found");
            }
            return (T)value;
        }
    }
}
