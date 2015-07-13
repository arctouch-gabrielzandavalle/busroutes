using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Collections.ObjectModel;


namespace busroutes
{
	public partial class DetailPage : TabbedPage
	{
		
		public DetailPage (Route route)
		{
			InitializeComponent ();
			this.Title = route.LongName;
			initializeChildren (route);
		}

		void initializeChildren (Route route)
		{

			this.Children.Add (new DeparturesContentPage (route));
			this.Children.Add (new StopsContentPage (route));
		}
	

  }
}

