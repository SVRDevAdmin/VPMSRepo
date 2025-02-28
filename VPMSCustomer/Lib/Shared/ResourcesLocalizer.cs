using Microsoft.Extensions.Localization;
using System.Reflection;
using VPMSCustomer.Interface;

namespace VPMSCustomer.Shared
{
    public class ResourcesLocalizer : IResourcesLocalizer
    {
        private readonly IStringLocalizer _localizer;

        public ResourcesLocalizer(IStringLocalizerFactory factory)
        {
            var type = typeof(LanguageResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _localizer = factory.Create("Resource", assemblyName.Name);
        }

        public LocalizedString this[String Key] => _localizer[Key];

        public LocalizedString GetLocalizedString(String Key)
        {
            return _localizer[Key];
        }
    }
}
