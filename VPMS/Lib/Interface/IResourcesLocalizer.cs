using Microsoft.Extensions.Localization;

namespace VPMSWeb.Interface
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