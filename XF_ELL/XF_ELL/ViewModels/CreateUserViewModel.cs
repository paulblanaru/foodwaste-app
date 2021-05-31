using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Services;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XF_ELL.DTO;
using XF_ELL.Parameters;
using XF_ELL.Results;

namespace XF_ELL.ViewModels
{
    public class CreateUserViewModel : MvxViewModel<CreateUserParameter, CreateUserResult>
    {
        private IMvxNavigationService _navigationService;
        private IUserService _userService;
        private string _username;
        private string _address;
        private DateTime _birthDate;
        private string _phone;
        private string _password;
        private byte[] _userPhotoBytes;
        private string _error = string.Empty;
        private bool _isAdded;
        private string _titleText = "New user";
        private ImageSource _userImageSource;
        public CreateUserViewModel(IMvxNavigationService navigationService, IUserService userService)
        {
            _navigationService = navigationService;
            _userService = userService;
            BackCommand = new MvxAsyncCommand(Back);
            AddCommand = new MvxAsyncCommand(Add);
            ChoosePhotoCommand = new MvxAsyncCommand(OnChoosePhoto);
        }

        public string TitleText
        {
            get => _titleText;
            set => SetProperty(ref _titleText, value);
        }

        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }
        public string Address
        {
            get => _address;
            set => SetProperty(ref _address, value);
        }
        public DateTime BirthDate
        {
            get => _birthDate;
            set => SetProperty(ref _birthDate, value);
        }

        public string Phone
        {
            get => _phone;
            set => SetProperty(ref _phone, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public byte[] UserPhotoBytes
        {
            get => _userPhotoBytes;
            set => SetProperty(ref _userPhotoBytes, value);
        }

        public ImageSource UserImageSource
        {
            get => _userImageSource;
            set => SetProperty(ref _userImageSource, value);
        }

        public string Error
        {
            get => _error;
            set => SetProperty(ref _error, value);
        }

        public override void Prepare(CreateUserParameter parameter)
        {
            this._isAdded = parameter.IsAdded;
        }

        public ICommand BackCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand ChoosePhotoCommand { get; }

        private async Task Back()
        {
            await _navigationService.Close(this);
        }

        private async Task Add()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Address) ||
                string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(Phone))
            {
                Error = Resources.Resources.CredentialsError;
                return;
            }

            UserDialogs.Instance.ShowLoading(Resources.Resources.LoadingText);

            try
            {
                var newUser = await _userService.Save(new UserDTO
                {
                    Name = Username,
                    Adress = Address,
                    Phone = Phone,
                    BirthDate = BirthDate,
                    Password = Password,
                    EncodedImage = Convert.ToBase64String(UserPhotoBytes)

                }) ;
                var result = new CreateUserResult { IsAdded = true, NewUsername = Username };

                await _navigationService.Close(this, result);
            }
            catch
            {
                Error = Resources.Resources.ErrorText;
            }
            UserDialogs.Instance.HideLoading();
        }

        private async Task OnChoosePhoto()
        {
            await CrossMedia.Current.Initialize();

            var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = $"{DateTime.Now}.jpg",
                SaveToAlbum = true,
                PhotoSize = PhotoSize.Small,
                DefaultCamera = CameraDevice.Front
            });

            if (file == null)
            {
                return;
            }

            var bytes = File.ReadAllBytes(file.Path);
            UserPhotoBytes = bytes;
            UserImageSource = ImageSource.FromStream(() => new MemoryStream(bytes));
        }
    }
}
