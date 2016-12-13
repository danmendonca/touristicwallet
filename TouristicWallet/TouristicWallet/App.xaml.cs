using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristicWallet.Data;
using TouristicWallet.Models;
using TouristicWallet.utils;
using Xamarin.Forms;

namespace TouristicWallet
{
    public partial class App : Application
    {

        public static MasterDetailPage MasterDetail { get; set; }

        public async static Task NavigateMasterDetail(Page page)
        {

            if(Device.Idiom != TargetIdiom.Desktop)
                MasterDetail.IsPresented = false;
            await MasterDetail.Detail.Navigation.PushAsync(page);
        }

        public App()
        {
            InitializeComponent();

            CurrencyScrapping.PrepareCurrencies();
            MainPage = new Views.MainPage();
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
