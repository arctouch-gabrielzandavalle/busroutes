using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace busroutes
{
	public partial class StopsContentPage : ContentPage
	{
		public StopsContentPage (Route route)
		{

			initialize (route);
		}

		async void initialize (Route route)
		{
			this.Title = "Stops";
			this.Content = new ListView {
				ItemsSource = await FindStopsByRouteId (route.Id),
				ItemTemplate = new DataTemplate (typeof(TextCell)) {
					Bindings =  {
						{
							TextCell.TextProperty,
							new Binding ("Name")
						}
					}
				}
			};
		}

		public async Task<List<Stop>> FindStopsByRouteId (Int32 routeId)
		{

			String jsonStops = "";

			await Task.Run( () =>
				{
					String postParam = "{\"params\": {\"routeId\": \"" + routeId + "\"}}";
					jsonStops = new WebRequest_BeginGetResponse ().execute ("/findStopsByRouteId/run",postParam);

				});

			return parseJson (jsonStops);
		}

		List<Stop> parseJson (string jsonRoutes)
		{
			var routeJsonObj = JObject.Parse (jsonRoutes);
			JArray array = (JArray)routeJsonObj ["rows"];
			List<Stop> stops = new List<Stop> ();
			foreach (JObject o in array.Children<JObject> ()) {
				Stop departure = new Stop ();
				departure.Id = (int)o ["id"];
				departure.Name = (String)o ["name"];
				stops.Add (departure);
			}
			return stops;
		}
	}
}

