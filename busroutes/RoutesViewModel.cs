using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Windows.Input;

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

		private Command _searchButtonPressed;
		public ICommand SearchButtonPressed
		{
			get
			{
				if (_searchButtonPressed == null)
				{
					_searchButtonPressed = new Command(FindRoutesByName);
				}

				return _searchButtonPressed;
			}
		}

		public RoutesViewModel ()
		{
			this.isActivityIndicatorRunning = false;
		}

		async void showNoRoutesFoundAlert ()
		{
			
			await MainPage.DisplayAlert ("Alert", "No routes Found!", "OK");
		}

		public async void FindRoutesByName ()
		{
			startActivityIndicator ();
			Routes =  await new  BusRoutesRestClient().FindRoutesByName (searchBarText);
			stopActivityIndicator ();

			if(Routes.Count == 0){
				showNoRoutesFoundAlert ();
			}
		}

		public void startActivityIndicator(){
			this.isActivityIndicatorRunning = true;
		}

		public void stopActivityIndicator(){
			this.isActivityIndicatorRunning = false;
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

