using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;
using XF_ELL.ViewModels;

namespace XF_ELL.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginView : MvxContentPage<LoginViewModel>
	{
		public LoginView ()
		{
			InitializeComponent ();
		}
	}
}