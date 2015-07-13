using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace busroutes
{
	public class Departure
	{

		public Int32 Id { get; set;}
		public String Time { get; set;}
		public String Calendar { get; set;}

		public Departure(){
		}

		public Departure (Int32 Id, String time,String calendar){
			this.Id = Id;
			this.Time = time;
			this.Calendar = calendar;
		}
			
	}


}

