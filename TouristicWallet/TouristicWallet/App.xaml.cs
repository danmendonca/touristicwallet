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
            MasterDetail.IsPresented = false;
            await MasterDetail.Detail.Navigation.PushAsync(page);
        }

        public App()
        {
            InitializeComponent();

            CurrencyScrapping.PrepareCurrencies();

            /* //teste code
            Wallet w = new Wallet(2);
            w.Amount = 10;
            Wallet w1 = new Wallet(3);
            Wallet w2 = new Wallet(4);
            Wallet w3 = new Wallet(5);
            w1.Amount = 20;
            w2.Amount = 30;
            w3.Amount = 40;
            WalletDataAccess.Instance.InsertOrUpdateWallet(w);
            WalletDataAccess.Instance.InsertOrUpdateWallet(w1);
            WalletDataAccess.Instance.InsertOrUpdateWallet(w2);
            WalletDataAccess.Instance.InsertOrUpdateWallet(w3);

            MainPage = new NavigationPage( new Views.ConvertPage("EUR"));*/
            //MainPage = new NavigationPage(new Views.WalletPage());
            MainPage = new Views.MainPage();
            //ViewModels.ViewModelBase.Navigation = MainPage.Navigation;
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
