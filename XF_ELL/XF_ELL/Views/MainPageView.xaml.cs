using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using MvvmCross;
using MvvmCross.Core;
using System.Reflection;
using Xamarin.Forms.Xaml;
using XF_ELL.ViewModels;
using MvvmCross.Forms.Views;

namespace XF_ELL.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPageView : MvxContentPage<MainPageViewModel>
    {
        public MainPageView()
        {
            InitializeComponent();
        }
    }
}
