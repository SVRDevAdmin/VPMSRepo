using Microsoft.Extensions.Localization;

namespace VPMSWeb.Lib.Interface
{
    public interface ISharedViewLocalizer
    {
        public LocalizedString this[string key]
        {
            get;
        }

        LocalizedString GetLocalizedString(string key);
    }

    public class SharedResource
    {
    }

}
