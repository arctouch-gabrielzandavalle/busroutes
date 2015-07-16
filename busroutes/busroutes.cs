using System;

using Xamarin.Forms;

namespace busroutes
{
	public class App : Application
	{

		static NavigationPage _NavPage;

		public App ()
		{
			// The root page of your application
			//MainPage = new NavigationPage(new SearchPage());
			MainPage = GetMainPage();
		}

		public static Page GetMainPage ()
		{
			var searchPage = new SearchPage();

			_NavPage = new NavigationPage(searchPage);

			return _NavPage;
		}

		public static bool IsLoggedIn {
			get { return !string.IsNullOrWhiteSpace(_Token); }
		}

		static string _Token;
		public static string Token {
			get { return _Token; }
		}

		public static void SaveToken(string token)
		{
			_Token = token;
		}

		public static Action SuccessfulLoginAction
		{
			get {
				return new Action (() => {
					_NavPage.Navigation.PopModalAsync();
				});
			}
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

