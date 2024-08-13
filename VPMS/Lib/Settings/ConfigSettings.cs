namespace VPMSWeb.Lib.Settings
{
	public class ConfigSettings
	{
		public static IConfiguration GetConfigurationSettings()
		{
			var iHost = Host.CreateApplicationBuilder();
			return iHost.Configuration;
		}
	}
}