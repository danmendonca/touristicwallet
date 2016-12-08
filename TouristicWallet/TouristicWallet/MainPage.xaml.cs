using System;
using TouristicWallet.models;
using Xamarin.Forms;

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
            Currency.GetAllCurrencies();
        }

    }
}
