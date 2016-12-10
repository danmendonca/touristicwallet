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

namespace TouristicWallet.ViewModels
{
    public class WalletPageViewModel : ViewModelBase
    {


        private List<Wallet> _currenciesWallet;
        public List<Wallet> Wallet {
            get { return _currenciesWallet; }
            set { _currenciesWallet = value; OnPropertyChanged(nameof(Wallet)); }
        }

        private string _defaultText;
        public string DefaultText
        {
            get { return _defaultText; }
            set { _defaultText = value; }
        }

        public ICommand GoToManagement { get; set; }


        public WalletPageViewModel()
        {
            Wallet = new List<Wallet>(); //todo load from db
            DefaultText = "Hello";


            /*
             * Dummy content
             * */
            CurrencyDataAccess cda = new CurrencyDataAccess();
            List<Currency> currencies = cda.GetCurrencies().ToList();

            List<Currency> some = currencies.Where(c => c.Initials.Contains("E") == true).ToList();

            foreach (var item in some)
            {
                Wallet w = new Wallet();
                w.Amount = 0.0;
                w.CurrencyId = item.Id;
                Wallet.Add(w);
            }
            /* end dummy content */

            OnPropertyChanged(nameof(Wallet));

            this.GoToManagement = new Command ( async () => await Navigation.PushAsync(new Views.WalletManagementPage()) );
        }

    }
}
