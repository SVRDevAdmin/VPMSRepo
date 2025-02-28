using Microsoft.Extensions.Localization;

namespace VPMSCustomer.Interface
{
    public interface IResourcesLocalizer
    {
        public LocalizedString this[String key]
        {
            get;
        }

        LocalizedString GetLocalizedString(String key);
    }

    public class LanguageResource
    {
    }
}
