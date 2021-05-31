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
using Services;
using Xamarin.Forms;
using System.IO;
using System.Threading;

namespace XF_ELL.ViewModels
{
    public class ProductDetailsViewModel : MvxViewModel<ProductDetailsParameter, ProductDetailsResult>
    {
        private IMvxNavigationService _navigationService;
        private IProductService _productService;
        private IUserService _userService;
        private int _productId;
        private string _name;
        private int _quantity;
        private DateTime _postedDate;
        private DateTime _expirationDate;
        private string _assignedUser;
        private string _address;
        private string _phone;
        private ImageSource _productImageSource;
        public int ProductId
        {
            get => _productId;
            set => SetProperty(ref _productId, value);
        }
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
        public int Quantity
        {
            get => _quantity;
            set => SetProperty(ref _quantity, value);
        }
        public DateTime PostedDate
        {
            get => _postedDate;
            set => SetProperty(ref _postedDate, value);
        }
        public DateTime ExpirationDate
        {
            get => _expirationDate;
            set => SetProperty(ref _expirationDate, value);
        }

        public string AssignedUser
        {
            get => _assignedUser;
            set => SetProperty(ref _assignedUser, value);
        }

        public ImageSource ProductImageSource
        {
            get => _productImageSource;
            set => SetProperty(ref _productImageSource, value);
        }

        public string Phone
        {
            get => _phone;
            set => SetProperty(ref _phone, value);
        }

        public string Address
        {
            get => _address;
            set => SetProperty(ref _address, value);
        }
        public ProductDetailsViewModel(IMvxNavigationService navigationService, IProductService productService,IUserService userService)
        {
            _navigationService = navigationService;
            _productService = productService;
            _userService = userService;
            BackCommand = new MvxAsyncCommand(Back);
            MapCommand = new MvxAsyncCommand(OnMap);          
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
                AssignedUser = productDto.AsignedUser;
                ProductImageSource = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(productDto.EncodedImage)));

                var userDto = await _userService.GetByName(AssignedUser);
                Address = userDto.Adress;
                Phone = userDto.Phone;
            }
            catch (Exception)
            {

            }
            UserDialogs.Instance.HideLoading();
        }

        

        public override void Prepare(ProductDetailsParameter parameter)
        {
            _productId = parameter.ProductId;            
        }

        public ICommand BackCommand { get; }
        public ICommand MapCommand { get; }
        
        private async Task OnMap()
        {
            var choices = new[] { "0h - 1h", "1h - 2h", "2h - 3h" };

            var choice = await UserDialogs.Instance.ActionSheetAsync("Your contribuitor wants to know when you arrive", "Sorry , I'm not intersted anymore", "Destroy", CancellationToken.None, choices);

            if (!string.IsNullOrEmpty(choice))
            {
                var param = new MapParameter() { Address = Address, 
                                                 Phone = Phone      };
                await _navigationService.Navigate<MapViewModel, MapParameter>(param);
            }
            else
            {
                await _navigationService.Close(this);
            }
            
        }

        private async Task Back()
        {
            await _navigationService.Close(this);
        }


        
    }
}
