using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TouristicWallet.utils;
using Xamarin.Forms;

namespace TouristicWallet
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            CurrencyScrapping.PrepareCurrencies();

            MainPage = new NavigationPage( new Views.WalletPage());
            ViewModels.ViewModelBase.Navigation = MainPage.Navigation;
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
