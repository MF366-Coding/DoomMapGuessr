using DoomMapGuessr.Services;

using IniParser.Model;


namespace DoomMapGuessr.Helpers
{

	internal static class IniDataSettingsExtensions
	{

		internal static void Save(this IniData data, string name) => ApplicationSettings.Shared.SetResourceFile(name, data);

		internal static void Save(this IniData data, string name, ApplicationSettings settings) => settings.SetResourceFile(name, data);

	}

}
