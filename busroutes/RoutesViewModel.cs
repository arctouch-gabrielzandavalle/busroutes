using System;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace busroutes
{
	public class RoutesViewModel : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged implementation
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion

		private String _searchBarText;
		public String searchBarText

		{
			get { return _searchBarText; }
			set
			{
				if (_searchBarText != value)
				{
					_searchBarText = value;
					PropertyChanged(this, new PropertyChangedEventArgs("searchBarText"));
				}
			}
		}

		private bool _isActivityIndicatorRunning;
		public bool isActivityIndicatorRunning
					
		{
			get { return _isActivityIndicatorRunning; }
			set
			{
				if (_isActivityIndicatorRunning != value)
				{
					_isActivityIndicatorRunning = value;
					PropertyChanged(this, new PropertyChangedEventArgs("isActivityIndicatorRunning"));
				}
			}
		}

		private ObservableCollection<Route> routes;

		public ObservableCollection<Route> Routes
		{
			get
			{
				return this.routes;
			}

			set
			{
				if (this.routes != value)
				{
					this.routes = value;
					PropertyChanged(this, new PropertyChangedEventArgs("Routes"));
				}
			}
		}

		public RoutesViewModel ()
		{
			this.isActivityIndicatorRunning = false;
		}

		public async void FindRoutesByName ()
		{
			startActivityIndicator ();
			Routes =  await new  BusRoutesRestClient().FindRoutesByName (searchBarText);
			stopActivityIndicator ();
		}

		public void startActivityIndicator(){
			this.isActivityIndicatorRunning = true;
		}

		public void stopActivityIndicator(){
			this.isActivityIndicatorRunning = false;
		}

	}
}

