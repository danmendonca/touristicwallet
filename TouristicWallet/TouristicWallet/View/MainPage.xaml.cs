using System;
using TouristicWallet.models;
using Xamarin.Forms;
using TouristicWallet.utils;
using TouristicWallet.Data;
using TouristicWallet.View;

namespace TouristicWallet
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            CurrencyScrapping.PrepareCurrencies();
            Navigation.PushAsync(new ConversionPage());
        }
    }
}
