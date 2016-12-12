using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristicWallet;
using TouristicWallet.Data;
using Xamarin.Forms;

namespace TouristicWallet.Views
{
    public partial class WalletPage : ContentPage
    {
        public WalletPage()
        {
            InitializeComponent();
            ViewModel.picker = picker;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.Update();
        }
    }
}
