// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using PrismAria.Models;

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

        private const string profileKey = "profile_key";
        private static readonly string profile = string.Empty;

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

        public static string Profile {
            get
            {
                return AppSettings.GetValueOrDefault(profileKey, profile);
            }
            set
            {
                AppSettings.AddOrUpdateValue(profileKey, value);
            }
        }

        public static void ClearEverything() {
            AppSettings.Clear();
        }

	}
}