using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XF_ELL.Parameters;
using XF_ELL.Results;
using XF_ELL.Services;

namespace XF_ELL.ViewModels
{
    public class ProductViewModel : MvxViewModel<ProductParameter>
    {
        private IMvxNavigationService _navigationService;
        private IProductService _productsService;
        private string _loggedInUsername;
        
        private ObservableCollection<ProductItemViewModel> _products;

        
        public ProductViewModel(IMvxNavigationService navigationService, IProductService productsService)
        {
            _navigationService = navigationService;
            _productsService = productsService;
            OnBackCommand = new MvxAsyncCommand(Back);
            SelectCommand = new MvxAsyncCommand<ProductItemViewModel>(SelectedProduct);
        }

        public ObservableCollection<ProductItemViewModel> Products
        {
            get => _products;
            set
            {
                SetProperty(ref _products, value);
                RaisePropertyChanged(() => HasItems);
            }
        }

        public override async Task Initialize()
        {
            UserDialogs.Instance.ShowLoading("Waiting for data...");
            try
            {
                var products = await _productsService.GetAll();
                Products = new ObservableCollection<ProductItemViewModel>(products.Select(e => new ProductItemViewModel
                {
                    ProductId = e.ProductId,
                    Name = e.Name,
                    ExpirationDate = e.ExpirationDate.ToShortDateString(),
                    AsignedUser = e.AsignedUser,
                    ProductImageSource = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(e.EncodedImage)))

                }));
            }
            catch (Exception)
            {
                UserDialogs.Instance.Toast("Couldn't get data!");
            }
            UserDialogs.Instance.HideLoading();
        }
        public ICommand SelectCommand { get; }
        public ICommand OnBackCommand { get; }
        public override void Prepare(ProductParameter parameter)
        {
            _loggedInUsername = parameter.Username;
        }
        private async Task Back()
        {
            await _navigationService.Close(this);
        }

        private async Task SelectedProduct(ProductItemViewModel productItemViewModel)
        {
            if (productItemViewModel.AsignedUser == _loggedInUsername)
            {
                var param = new EditProductParameter() { ProductId = productItemViewModel.ProductId };
                var result = await _navigationService.Navigate<EditProductViewModel, EditProductParameter, EditProductResult>(param);
                if (result == null)
                {
                    return;
                }
                else
                {
                    productItemViewModel.Name = result.NewName;
                    productItemViewModel.ExpirationDate = result.NewExpirationDate.ToString();
                    productItemViewModel.ProductImageSource = result.NewProductImageSource;
                    
                    return;
                }
            }
            else
            {
                var param = new ProductDetailsParameter() { ProductId = productItemViewModel.ProductId };
                var result = await _navigationService.Navigate<ProductDetailsViewModel, ProductDetailsParameter, ProductDetailsResult>(param);
                if (result == null)
                {
                    return;
                }


                if (result.Deleted)
                {
                    Products.Remove(productItemViewModel);
                    return;
                }
            }
        }

        public bool HasItems
        {
            get
            {
                if (_products != null)
                {
                    return _products.Count > 0;
                }
                return false;
            }
        }
        
    }
}
