using System;
using Xamarin.Forms;
using Android.Gms.Maps;
using Xamarin;


[assembly: Dependency(typeof(busroutes.Droid.AndroidMap))]
namespace busroutes.Droid
{
	public class AndroidMap : IMap
	{
		public AndroidMap ()
		{
		}

		public String getSelectedAddress(){
			
			return "teste";
		}
	}
}

