using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristicWallet;

using Xamarin.Forms;

namespace TouristicWallet.Views
{
    public partial class WalletPage : ContentPage
    {
        public WalletPage()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            NavigationPage navPage = (NavigationPage)App.Current.MainPage;
            IReadOnlyList<Page> modStack = navPage.Navigation.ModalStack;
            int modalCount = modStack.Count;
            bool noModals = modalCount == 0 || (modalCount == 1 && modStack[0] is NavigationPage);

            if (noModals)
            {
                return base.OnBackButtonPressed();
            }
            return true;
        }
    }
}
