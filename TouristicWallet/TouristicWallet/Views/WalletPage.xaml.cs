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
        static Picker pick;

        public WalletPage()
        {
            InitializeComponent();
            foreach (var item in WalletDataAccess.Instance.GetOwned())
            {
                picker.Items.Add(item.Currency.Initials);
            }
            picker.SelectedIndex = 0;
            pick = picker;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.Update();
        }

        public static string getSelectedCurrency()
        {
            return pick.Items[pick.SelectedIndex];
        }
    }
}
