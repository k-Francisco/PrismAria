// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace PrismAria.Helpers
{
	/// <summary>
	/// This is the Settings static class that can be used in your Core solution or in any
	/// of your client applications. All settings are laid out the same exact way with getters
	/// and setters. 
	/// </summary>
	public static class Settings
	{
		private static ISettings AppSettings
		{
			get
			{
				return CrossSettings.Current;
			}
		}

		#region Setting Constants

		private const string tokenKey = "token_key";
		private static readonly string tokenDefault = string.Empty;

		#endregion


		public static string Token
		{
			get
			{
				return AppSettings.GetValueOrDefault(tokenKey, tokenDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue(tokenKey, value);
			}
		}

	}
}