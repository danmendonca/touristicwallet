using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace TouristicWallet.Views
{
    public partial class WalletManagementPage : ContentPage
    {
        public WalletManagementPage()
        {
            InitializeComponent();
        }

        public void Toggled_handler(object sender, ToggledEventArgs e)
        {
            Boolean a = e.Value;
            if (a)
            {
                a = !a;
            }
        }
    }
}
