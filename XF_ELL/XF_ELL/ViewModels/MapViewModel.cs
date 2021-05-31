using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using XF_ELL.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;

namespace XF_ELL.ViewModels
{
    public class MapViewModel : MvxViewModel<MapParameter>
    {

        private IMvxNavigationService _navigationService;
        private double _latitude;
        private double _longitude;
        private string _address;
        private string _titleText;

        public MapViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            OnBackCommand = new MvxAsyncCommand(Back);
           
        }

        public double Latitude
        {
            get => _latitude;
            set => SetProperty(ref _latitude, value);
        }
        public double Longitude
        {
            get => _longitude;
            set => SetProperty(ref _longitude, value);
        }
        
        public string Address
        {
            get => _address;
            set => SetProperty(ref _address, value);
        }

        public string TitleText
        {
            get => _titleText;
            set => SetProperty(ref _titleText, value);
        }

        public override async Task Initialize()
        {
            var locations = await Geocoding.GetLocationsAsync(_address);
            var location = locations.FirstOrDefault();
            UserDialogs.Instance.ShowLoading(Resources.Resources.LoadingText);
            try
            {
                Latitude = location.Latitude;
                Longitude = location.Longitude;
            }
            catch (Exception)
            {
                UserDialogs.Instance.Toast(Resources.Resources.ErrorText);
            }
            UserDialogs.Instance.HideLoading();
        }

        public override void Prepare(MapParameter parameter)
        {
            _address = parameter.Address;
            _titleText = _address;
        }

        public ICommand OnBackCommand { get; }

        private async Task Back()
        {
            await _navigationService.Close(this);
        }
    }
}
