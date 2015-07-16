using System;

using Xamarin.Forms;

namespace busroutes
{
	public class ProfilePage : BaseContentPage
	{
		public ProfilePage ()
		{
			Content = new Label () {
				Text = "Profile Page", 
				VerticalOptions = LayoutOptions.CenterAndExpand,
				HorizontalOptions = LayoutOptions.CenterAndExpand, 
			};
		}
	}
}


