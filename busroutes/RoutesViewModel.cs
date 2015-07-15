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
		public String SearchBarText

		{
			get { return _searchBarText; }
			set
			{
				if (_searchBarText != value)
				{
					_searchBarText = value;
					PropertyChanged(this, new PropertyChangedEventArgs("SearchBarText"));
				}
			}
		}

		private bool _isActivityIndicatorRunning;
		public bool IsActivityIndicatorRunning
					
		{
			get { return _isActivityIndicatorRunning; }
			set
			{
				if (_isActivityIndicatorRunning != value)
				{
					_isActivityIndicatorRunning = value;
					PropertyChanged(this, new PropertyChangedEventArgs("IsActivityIndicatorRunning"));
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
			this.IsActivityIndicatorRunning = false;
		}

		async void ShowNoRoutesFoundAlert ()
		{
			
			await MainPage.DisplayAlert ("Alert", "No routes Found!", "OK");
		}

		public async void FindRoutesByName ()
		{
			StartActivityIndicator ();
			Routes =  await new  BusRoutesRestClient().FindRoutesByName (SearchBarText);
			StopActivityIndicator ();

			if(Routes.Count == 0){
				ShowNoRoutesFoundAlert ();
			}
		}

		public void StartActivityIndicator(){
			this.IsActivityIndicatorRunning = true;
		}

		public void StopActivityIndicator(){
			this.IsActivityIndicatorRunning = false;
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

