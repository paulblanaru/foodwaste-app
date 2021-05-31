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
using XF_ELL.Resources;
using XF_ELL.DTO;
using Xamarin.Forms;
using System.IO;
using Plugin.Media;
using Plugin.Media.Abstractions;

namespace XF_ELL.ViewModels
{
    public class EditProductViewModel : MvxViewModel<EditProductParameter, EditProductResult>
    {
        private IMvxNavigationService _navigationService;
        private IProductService _productService;
        private int _productId;
        private string _username;
        private int _quantity;
        private DateTime _postedDate;
        private DateTime _expirationDate;
        private string _asignedUser;
        private string _name;
        private string _error = string.Empty;
        private string _titleText;
        private bool _isUpdate;
        private ICommand _updateCommand;
        private ImageSource _productImageSource;
        private byte[] _productPhotoBytes;

        public EditProductViewModel(IMvxNavigationService navigationService, IProductService productService)
        {
            _navigationService = navigationService;
            _productService = productService;
            BackCommand = new MvxAsyncCommand(Back);
            UpdateCommand = new MvxAsyncCommand(Update);
            ChoosePhotoCommand = new MvxAsyncCommand(OnChoosePhoto);
        }

        public override async Task Initialize()
        {
            UserDialogs.Instance.ShowLoading(Resources.Resources.LoadingText);
            try
            {
                var productDto = await _productService.GetById(_productId);
                ProductId = productDto.ProductId;
                Name = productDto.Name;
                Quantity = productDto.Quantity;
                PostedDate = productDto.PostedDate;
                ExpirationDate = productDto.ExpirationDate;
                AsignedUser = productDto.AsignedUser;
                ProductPhotoBytes = Convert.FromBase64String(productDto.EncodedImage);
                ProductImageSource = ImageSource.FromStream(() => new MemoryStream(ProductPhotoBytes));
            }
            catch (Exception)
            {

            }
            UserDialogs.Instance.HideLoading();
        }

        public override void Prepare(EditProductParameter parameter)
        {
            this._productId = parameter.ProductId;
        }

        public string TitleText
        {
            get => _titleText;
            set => SetProperty(ref _titleText, value);
        }

        public int ProductId
        {
            get => _productId;
            set => SetProperty(ref _productId, value);
        }
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
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
        public DateTime PostedDate
        {
            get => _postedDate;
            set => SetProperty(ref _postedDate, value);
        }
        public string AsignedUser
        {
            get => _asignedUser;
            set => SetProperty(ref _asignedUser, value);
        }
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string Error
        {
            get => _error;
            set => SetProperty(ref _error, value);
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

        public ICommand ChoosePhotoCommand {get;}
        public ICommand AsignCommand { get; }
        public ICommand BackCommand { get; }
        public ICommand UpdateCommand 
        {
            get => _updateCommand;
            set => SetProperty(ref _updateCommand, value);
        }

        private async Task Back()
        {
            await _navigationService.Close(this);
        }

        private async Task Update()
        {
            if (string.IsNullOrWhiteSpace(Name) || Quantity == 0 || ExpirationDate < DateTime.Now || ProductImageSource == null)
            {
                Error = Resources.Resources.CredentialsError;
                return;
            }
            UserDialogs.Instance.ShowLoading(Resources.Resources.LoadingText);
            try
            {
                await _productService.Update(_productId, new ProductDTO()
                {
                    ProductId = ProductId,
                    Name = Name,
                    PostedDate = DateTime.Now,
                    ExpirationDate = ExpirationDate,
                    Quantity = Quantity,
                    AsignedUser = AsignedUser,
                    EncodedImage = Convert.ToBase64String(ProductPhotoBytes)

                });
                var result = new EditProductResult
                {
                    NewName = Name,
                    NewExpirationDate = ExpirationDate.ToShortDateString(),
                    NewPostedDate = PostedDate.ToShortDateString(),
                    NewQuantity = Quantity,
                    NewProductImageSource = ProductImageSource
                };
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
            ProductPhotoBytes = bytes;
            ProductImageSource = ImageSource.FromStream(() => new MemoryStream(bytes));
        }
    }
}
