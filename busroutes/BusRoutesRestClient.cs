using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.ObjectModel;


namespace busroutes
{
	public class BusRoutesRestClient
	{
		public BusRoutesRestClient ()
		{
			
		}

		public async Task<ObservableCollection<Route>> FindRoutesByName(String routeName){

			String jsonRoutes = "";

			await Task.Run( () =>
				{
					String postParam = "{\"params\": {\"stopName\": \"%" + routeName + "%\"}}";
					jsonRoutes = new WebRequest_BeginGetResponse ().Execute ("/findRoutesByStopName/run",postParam);

			});

			return parseJson (jsonRoutes);
		}

		//public List<Route> FilterRoutes(String text){
		//	return this.routes.FindAll(x => x.LongName.Contains(text));
		//}


		ObservableCollection<Route> parseJson (string jsonRoutes)
		{
			var routeJsonObj = JObject.Parse (jsonRoutes);
			JArray array = (JArray)routeJsonObj ["rows"];
			ObservableCollection<Route> routeList = new ObservableCollection<Route> ();
			foreach (JObject o in array.Children<JObject> ()) {
				Route route = new Route ();
				route.Id = (int)o ["id"];
				route.LongName = (String)o ["longName"];
				route.shortName = (String)o ["shortName"];
				routeList.Add (route);
			}
			return routeList;
		}

	}
}

