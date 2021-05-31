using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XF_ELL.ViewModels
{
     
    public class ProductItemViewModel : MvxNotifyPropertyChanged
    {
        private int _productId;
        private string _name;
        private string _expirationDate;
        private string _asignedUser;
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

        public string ExpirationDate 
        {
            get => _expirationDate;
            set => SetProperty(ref _expirationDate, value); 
        }
        public string AsignedUser
        {
            get => _asignedUser;
            set => SetProperty(ref _asignedUser, value);
        }

        public ImageSource ProductImageSource
        {
            get => _productImageSource;
            set => SetProperty(ref _productImageSource, value);
        }
        public ProductItemViewModel(int productId, string name , string expirationDate, string asignedUser, ImageSource productImageSource)
        {
            _productId = productId;
            _name = name;
            _expirationDate = expirationDate;
            _asignedUser = asignedUser;
            _productImageSource = productImageSource;
        }

        public ProductItemViewModel()
        {

        }
    }
}
