using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristicWallet.Models;
using TouristicWallet.Data;
using System.Windows.Input;
using Xamarin.Forms;
using TouristicWallet.Views;

namespace TouristicWallet.ViewModels
{
    public class WalletPageViewModel : ViewModelBase
    {
        public Picker picker;
        private List<Wallet> _currenciesWallet;
        public List<Wallet> Wallet {
            get { return _currenciesWallet; }
            set { _currenciesWallet = value; OnPropertyChanged(nameof(Wallet)); }
        }

        public WalletPageViewModel()
        {
            WalletDataAccess wda = WalletDataAccess.Instance;
            Wallet = wda.GetOwned().ToList(); 
        }

        public void Update()
        {
            WalletDataAccess wda = WalletDataAccess.Instance;
            Wallet = wda.GetOwned().ToList();
        }
    }
}
