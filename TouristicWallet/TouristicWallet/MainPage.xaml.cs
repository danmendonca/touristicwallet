using System;
using TouristicWallet.models;
using Xamarin.Forms;
using TouristicWallet.utils;

namespace TouristicWallet
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        void OnButtonClicked(object sender, EventArgs args)
        {
            CurrencyScrapping.GetAllCurrencies();
        }

    }
}
