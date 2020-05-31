using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont("OpenSans-Regular.ttf", Alias = "Regular")]
[assembly: ExportFont("OpenSans-SemiBold.ttf", Alias = "SemiBold")]
[assembly: ExportFont("Font Awesome 5 Free-Regular-400.otf", Alias="FAR")]

namespace Pizza
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
