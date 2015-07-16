using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using busroutes.iOS;
using busroutes;
using Xamarin.Auth;

[assembly: ExportRenderer (typeof (LoginPage), typeof (LoginPageRenderer))]
namespace busroutes.iOS
{
	public class LoginPageRenderer : PageRenderer
	{
		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

			var auth = new OAuth2Authenticator (
				clientId: "476404872533398", // your OAuth2 client id
				scope: "", // the scopes for the particular API you're accessing, delimited by "+" symbols
				authorizeUrl: new Uri ("https://m.facebook.com/dialog/oauth/"),
				redirectUrl: new Uri ("http://www.facebook.com/connect/login_success.html")); // the redirect URL for the service

			auth.Completed += (sender, eventArgs) => {
				// We presented the UI, so it's up to us to dimiss it on iOS.
				App.SuccessfulLoginAction.Invoke();

				if (eventArgs.IsAuthenticated) {
					// Use eventArgs.Account to do wonderful things
					App.SaveToken(eventArgs.Account.Properties["access_token"]);
				} else {
					// The user cancelled
				}
			};

			PresentViewController (auth.GetUI (), true, null);
		}
	}

}

