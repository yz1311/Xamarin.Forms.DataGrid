using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace DataGridSample
{
    public class App : Application
    {
        public App()
        {
            // The root page of your application
            NavigationPage rootPage = new NavigationPage(new MainPage() { BindingContext = new SampleVM()});

            MainPage = rootPage;

        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
