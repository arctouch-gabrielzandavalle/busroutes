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
			Navigation.PushAsync(new MapPage (searchBar.Text ?? ""));
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			if (!App.IsLoggedIn) {
				Navigation.PushModalAsync(new LoginPage());
			}
		}
	}
}

