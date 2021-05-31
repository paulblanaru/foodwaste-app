using MvvmCross.Forms.Views;
using XF_ELL.ViewModels;
using Xamarin.Forms.Xaml;
using System.ComponentModel;

namespace XF_ELL.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	[DesignTimeVisible(true)]
	public partial class CreateUserView : MvxContentPage<CreateUserViewModel>
	{
		public CreateUserView ()
		{

			InitializeComponent ();
			
		}
	}
}