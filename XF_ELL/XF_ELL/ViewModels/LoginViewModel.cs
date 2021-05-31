using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using XF_ELL.Parameters;
using XF_ELL.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Services;

namespace XF_ELL.ViewModels
{
    public class LoginViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IUserService _userService;
        private string _username;
        private string _password;
        private string _error;

        public LoginViewModel(IMvxNavigationService navigationService, IUserService userService)
        {
            _navigationService = navigationService;
            _userService = userService;
            NoAccountCommand = new MvxAsyncCommand(NoAccount);
            ForgotCommand = new MvxAsyncCommand(ForgotPassword);
            LoginCommand = new MvxAsyncCommand(Login);
        }

        public override Task Initialize()
        {
            Username = string.Empty;
            Password = string.Empty;
            Error = string.Empty;
            return base.Initialize();
        }

        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public string Error
        {
            get => _error;
            set => SetProperty(ref _error, value);
        }

        public ICommand NoAccountCommand { get; }
        public ICommand LoginCommand { get; }
        public ICommand ForgotCommand { get; }

        private async Task NoAccount()
        {
            await _navigationService.Navigate<CreateUserViewModel>();
        }

        private async Task Login()
        {
            try
            {
                if (_username == null || _password == null)
                {
                    Error = Resources.Resources.CredentialsError;
                }
                else if (await _userService.Login(_username, _password))
                {
                    Error = null;
                    var param = new MainPageParameter { Username = _username };
                    await _navigationService.Navigate<MainPageViewModel,MainPageParameter>(param);
                }
                else
                {
                    Error = "Invalid user or password!";
                    Username = string.Empty;
                    Password = string.Empty;
                }
            }           
            catch (Exception ex)
            {
                Error = ex.Message;
            }
        }

        private async Task ForgotPassword()
        {
            await Task.Run(() => { Device.OpenUri(new Uri("https://www.google.com")); });
        }
    }
}
