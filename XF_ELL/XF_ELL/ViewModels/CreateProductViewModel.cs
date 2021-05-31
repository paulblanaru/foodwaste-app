using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using XF_ELL.Parameters;
using XF_ELL.Results;
using XF_ELL.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using XF_ELL.DTO;
using Xamarin.Forms;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.IO;

namespace XF_ELL.ViewModels
{
    public class CreateProductViewModel : MvxViewModel<CreateProductParameter, CreateProductResult>
    {
        private IMvxNavigationService _navigationService;
        private IProductService _productService;
        private string _name;
        private int _quantity;
        private DateTime _expirationDate;
        private string _error = string.Empty;
        private bool _isAdded;
        private string _asignedUser;
        private string _titleText = "New task";
        private byte[] _productPhotoBytes;
        private ImageSource _productImageSource;

        public CreateProductViewModel(IMvxNavigationService navigationService, IProductService productService)
        {
            _navigationService = navigationService;
            _productService = productService;
            BackCommand = new MvxAsyncCommand(Back);
            AddCommand = new MvxAsyncCommand(Add);
            ChoosePhotoCommand = new MvxAsyncCommand(OnChoosePhoto);
        }

        public string TitleText
        {
            get => _titleText;
            set => SetProperty(ref _titleText, value);
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
        public string AsignedUser
        {
            get => _asignedUser;
            set => SetProperty(ref _asignedUser, value);
        }
        public int Quantity
        {
            get => _quantity;
            set => SetProperty(ref _quantity, value);
        }
        public DateTime ExpirationDate
        {
            get => _expirationDate;
            set => SetProperty(ref _expirationDate, value);
        }
        public byte[] ProductPhotoBytes
        {
            get => _productPhotoBytes;
            set => SetProperty(ref _productPhotoBytes, value);
        }
        public ImageSource ProductImageSource
        {
            get => _productImageSource;
            set => SetProperty(ref _productImageSource, value);
        }
        public string Error
        {
            get => _error;
            set => SetProperty(ref _error, value);
        }

        public override void Prepare(CreateProductParameter parameter)
        {
            this._isAdded = parameter.IsAdded;
            this._asignedUser = parameter.AsignedUser; 
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
            if (string.IsNullOrWhiteSpace(Name) || Quantity.Equals(null))
            {
                Error = Resources.Resources.CredentialsError;
                return;
            }

            UserDialogs.Instance.ShowLoading(Resources.Resources.LoadingText);

            try
            {
                var newProduct = await _productService.Save(new ProductDTO
                {
                    Name = Name,
                    ExpirationDate = ExpirationDate,
                    Quantity = Quantity,
                    AsignedUser = AsignedUser,
                    EncodedImage = Convert.ToBase64String(ProductPhotoBytes)

                }) ;
                var result = new CreateProductResult { IsAdded = true, NewName = Name, NewExpirationDate = ExpirationDate , NewProductPhotoBytes = ProductPhotoBytes };

                await _navigationService.Close(this);
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
            ProductPhotoBytes = bytes;
            ProductImageSource = ImageSource.FromStream(() => new MemoryStream(bytes));
        }
    }
}
