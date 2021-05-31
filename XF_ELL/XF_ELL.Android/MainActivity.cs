using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using MvvmCross.Forms.Platforms.Android.Views;
using MvvmCross.Forms.Platforms.Android.Core;
using Acr.UserDialogs;
using MvvmCross;
using MvvmCross.Platforms.Android;
using Plugin.Media;
using Plugin.CurrentActivity;

namespace XF_ELL.Droid
{
    [Activity(Label = "XF_ELL", Icon = "@mipmap/icon", Theme = "@style/MainTheme",  ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : MvxFormsAppCompatActivity<MvxFormsAndroidSetup<MvxApp, App>, MvxApp, App>
    {
        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            CrossCurrentActivity.Current.Activity = this;
            Xamarin.FormsMaps.Init(this, bundle);
            Xamarin.Essentials.Platform.Init(this, bundle);

            await CrossMedia.Current.Initialize();



            UserDialogs.Init(() => Mvx.IoCProvider.Resolve<IMvxAndroidCurrentTopActivity>().Activity);
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            //Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}