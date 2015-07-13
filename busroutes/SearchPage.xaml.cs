using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Threading.Tasks;

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
			Route route = (Route) listView.SelectedItem;
			Navigation.PushAsync(new DetailPage(route));

		}

	}
}

