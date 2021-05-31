using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace XF_ELL.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomMap : MvxContentView
    {
        private static Map _map;
        private static double _latitude;
        private static double _longitude;

        public CustomMap()
        {
            InitializeComponent();
            Init();
        }

        #region BindableProperties
        public static readonly BindableProperty LatitudeProperty =
            BindableProperty.Create(
            nameof(Latitude),
            typeof(double),
            typeof(CustomMap),
            46.77,
            BindingMode.OneWay,
            propertyChanged: OnLatitudeChange);

        public double Latitude
        {
            get
            {
                return (double)GetValue(LatitudeProperty);
            }
            set
            {
                SetValue(LatitudeProperty, value);
            }
        }

        public static readonly BindableProperty LongitudeProperty =
           BindableProperty.Create(
           nameof(Longitude),
           typeof(double),
           typeof(CustomMap),
           23.59,
           BindingMode.OneWay,
           propertyChanged: OnLongitudeChange);

        public double Longitude
        {
            get
            {
                return (double)GetValue(LongitudeProperty);
            }
            set
            {
                SetValue(LongitudeProperty, value);
            }
        }

        private static void OnLatitudeChange(BindableObject bindable, object oldvalue, object newvalue)
        {
            _latitude = (double)newvalue;
            (bindable as CustomMap).Map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(_latitude, _longitude), Distance.FromMiles(1)));
            SetPin(_latitude, _longitude);
        }

        private static void OnLongitudeChange(BindableObject bindable, object oldvalue, object newvalue)
        {
            _longitude = (double)newvalue;
            (bindable as CustomMap).Map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(_latitude, _longitude), Distance.FromMiles(1)));
            SetPin(_latitude, _longitude);
        }
        #endregion

        public static void SetPin(double longitude, double latitude)
        {
            var pin = new Pin()
            {
                Position = new Position(longitude, latitude),
                Label = "Address"
            };
            _map.Pins.Clear();
            _map.Pins.Add(pin);
        }

        private void Init()
        {
            _map = FindByName(nameof(Map)) as Map;
        }

    }
}