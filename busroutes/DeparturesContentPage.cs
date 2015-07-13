using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace busroutes
{
	public partial class DeparturesContentPage : ContentPage
	{
		public DeparturesContentPage (Route selectedRoute)
		{

			var indicator = new ActivityIndicator() {
				HorizontalOptions = LayoutOptions.CenterAndExpand
			};

			this.Title = "Departures";
			var listView = new ListView {
				
				ItemTemplate = new DataTemplate (typeof(TextCell)) {
					Bindings = { {
							TextCell.TextProperty,
							new Binding ("Time")
						}
					}
				}
			};

			var root = new StackLayout() {
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				Children = {
					listView, 
					indicator
				}
			};

			this.Content = root;

			initialize (selectedRoute,listView,indicator);
		}

		async void  initialize (Route selectedRoute,ListView listView,ActivityIndicator indicator)
		{
			ActivateIndicator (indicator);
			listView.ItemsSource = await FindDeparturesByRouteId (selectedRoute.Id);
			DeactivateIndicator (indicator);
		}

		public async Task<List<Departure>> FindDeparturesByRouteId (Int32 routeId)
		{
			
			String jsonRoutes = "";

			await Task.Run( () =>
				{
					String postParam = "{\"params\": {\"routeId\": \"" + routeId + "\"}}";
					jsonRoutes = new WebRequest_BeginGetResponse ().execute ("/findDeparturesByRouteId/run",postParam);

				});

			return parseJson (jsonRoutes);
		}

		List<Departure> parseJson (string jsonRoutes)
		{
			var routeJsonObj = JObject.Parse (jsonRoutes);
			JArray array = (JArray)routeJsonObj ["rows"];
			List<Departure> departures = new List<Departure> ();
			foreach (JObject o in array.Children<JObject> ()) {
				Departure departure = new Departure ();
				departure.Id = (int)o ["id"];
				departure.Time = (String)o ["time"];
				departure.Calendar = (String)o ["calendar"];
				departures.Add (departure);
			}
			return departures;
		}

		void ActivateIndicator (ActivityIndicator activityIndicator)
		{
			activityIndicator.IsRunning = true;
			activityIndicator.IsVisible = true;
		}

		void DeactivateIndicator (ActivityIndicator activityIndicator)
		{
			activityIndicator.IsRunning = false;
			activityIndicator.IsVisible = false;
		}
	}
}

