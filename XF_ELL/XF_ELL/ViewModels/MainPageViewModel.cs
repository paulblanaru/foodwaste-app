using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using MvvmCross;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using XF_ELL.Parameters;
using Services;

namespace XF_ELL.ViewModels
{
    public class MainPageViewModel : MvxViewModel<MainPageParameter>
    {
        private string _loggedInUsername;
        private IMvxNavigationService _navigationService;
        private string _titleText;
        private string _address;
        private IUserService _userService;
        public MainPageViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            ProductsCommand = new MvxAsyncCommand(Products);
            DonateCommand = new MvxAsyncCommand(Donate);
        }

        public override Task Initialize()
        {            
            TitleText = "Welcome";
            return base.Initialize();
        }
        public override void Prepare(MainPageParameter parameter)
        {
            _loggedInUsername = parameter.Username;
        }
        public string TitleText
        {
            get => _titleText;
            set => SetProperty(ref _titleText, value);
        }

        public string Address
        {
            get => _address;
            set => SetProperty(ref _address, value);
        }

        public ICommand ProductsCommand { get; }
        public ICommand DonateCommand { get; }
        
        private async Task Products()
        {
            var param = new ProductParameter { Username = _loggedInUsername };
            await _navigationService.Navigate<ProductViewModel,ProductParameter>(param);
        }

        private async Task Donate()
        {
            var param = new CreateProductParameter() { AsignedUser = _loggedInUsername };
            await _navigationService.Navigate<CreateProductViewModel, CreateProductParameter>(param);
        }
        private async Task OnMap()
        {
            var param = new MapParameter() { Address = "Suceava" };
            await _navigationService.Navigate<MapViewModel, MapParameter>(param);
        }

    }
}

