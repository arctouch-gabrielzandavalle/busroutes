using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace busroutes
{
	
	public partial class SearchPage : ContentPage
	{
		public SearchPage ()
		{			
			InitializeComponent ();
			BindingContext = new RoutesViewModel ();
		}
				
		void onItemSelected(Object o, EventArgs e){
			listView = o as ListView;
			var route = (Route) listView.SelectedItem;
			Navigation.PushAsync(new DetailPage(route));
		}

		public void SearchRoutesOnMap(Object o, EventArgs e){
			var map = new Map(
				MapSpan.FromCenterAndRadius(
					new Position(37,-122), Distance.FromMiles(0.3))) {
				IsShowingUser = true,
				HeightRequest = 100,
				WidthRequest = 960,
				VerticalOptions = LayoutOptions.FillAndExpand
			};
			var stack = new StackLayout { Spacing = 0 };
			stack.Children.Add(map);
			Content = stack;
		}

	}
}

