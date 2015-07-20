using System;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace busroutes
{
	public class MapPage : ContentPage
	{
		public MapPage (String query)
		{
			initializeMap (query);

		}

		async void initializeMap (string query)
		{
			var map = new Map (MapSpan.FromCenterAndRadius (new Position (-27.591792, -48.543162), Distance.FromMiles (1))) {
				IsShowingUser = true,
				HeightRequest = 100,
				WidthRequest = 960,
				VerticalOptions = LayoutOptions.FillAndExpand
			};

			var stack = new StackLayout {
				Spacing = 0
			};

			if (!"".Equals (query)) {
				var Pin = await GetPin (query);

				if (Pin == null) {
					ShowStreetNotFound ();
				} else {
					map.Pins.Add (Pin);
					map.MoveToRegion(new MapSpan (Pin.Position, Pin.Position.Latitude, Pin.Position.Longitude));

				}
			}

			stack.Children.Add (map);
			Content = stack;

			//IMap nativeMap = DependencyService.Get<IMap> ();
			//testAlert (nativeMap.getSelectedAddress ());
		}

		async Task<Pin> GetPin(String query){
			
			List<Position> list = (await new Geocoder ().GetPositionsForAddressAsync (query)).ToList();

			if (list.Count == 0) {
				return null;
			}
				
			var position =  list.First () ;
			var pin = new Pin {
				Type = PinType.Place,
				Position = position,
				Label = "custom pin",
				Address = "custom detail info"
			};
			return pin;

		}

		async void ShowStreetNotFound ()
		{

			await  MainPage.DisplayAlert ("Alert", "Street not found.", "OK");
		}

		public NavigationPage MainPage
		{
			get
			{
				return (NavigationPage)((App)App.Current).MainPage;
			}
		}
	}
}


