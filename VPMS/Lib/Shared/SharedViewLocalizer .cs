using Microsoft.Extensions.Localization;
using System.Reflection;
using System.Runtime.CompilerServices;
using VPMSWeb.Lib.Interface;

namespace VPMSWeb.Lib.Shared
{
    public class SharedViewLocalizer : ISharedViewLocalizer
    {
        private readonly IStringLocalizer _localizer;

        public SharedViewLocalizer(IStringLocalizerFactory factory)
        {
            var type = typeof(SharedResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _localizer = factory.Create("Resource", assemblyName.Name);
        }

        public LocalizedString this[String key] => _localizer[key];

        public LocalizedString GetLocalizedString(String key)
        {
            return _localizer[key];
        }
    }
}
