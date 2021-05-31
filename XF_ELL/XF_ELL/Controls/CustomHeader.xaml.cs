using MvvmCross.Forms.Views;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XF_ELL.Controls
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomHeader : MvxContentView
    {
        public CustomHeader()
        {
            InitializeComponent();
            IsAddVisible = false;
        }

        public static readonly BindableProperty BackCommandProperty =
            BindableProperty.Create(
                nameof(BackCommand),
                typeof(ICommand),
                typeof(CustomHeader),
                null,
                BindingMode.OneWay,
                propertyChanged: OnBackPropertyChanged);

        private static void OnBackPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {

        }

        public string TitleText
        {
            get => (string)GetValue(TitleTextProperty);
            set => SetValue(TitleTextProperty, value);
        }

        private static readonly BindableProperty TitleTextProperty 
            = BindableProperty.Create(
                nameof(TitleText),
                typeof(string),
                typeof(CustomHeader),
                null,
                BindingMode.TwoWay);

        public ICommand BackCommand
        {
            get => (ICommand)GetValue(BackCommandProperty);
            set => SetValue(BackCommandProperty, value);
        }

        public static readonly BindableProperty AddCommandProperty =
           BindableProperty.Create(
               nameof(AddCommand),
               typeof(ICommand),
               typeof(CustomHeader),
               null,
               BindingMode.OneWay,
               propertyChanged: OnAddPropertyChanged);

        private static void OnAddPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {

        }

        public ICommand AddCommand
        {
            get => (ICommand)GetValue(AddCommandProperty);
            set => SetValue(AddCommandProperty, value);
        }

        private static readonly BindableProperty IsAddVisibleProperty
            = BindableProperty.Create(
                nameof(IsAddVisible),
                typeof(bool),
                typeof(CustomHeader),
                null,
                BindingMode.TwoWay);

        public bool IsAddVisible
        {
            get => (bool)GetValue(IsAddVisibleProperty);
            set => SetValue(IsAddVisibleProperty, value);
        }

    }
}
