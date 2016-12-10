using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TouristicWallet.Data;
using TouristicWallet.Models;
using Xamarin.Forms;

namespace TouristicWallet.ViewModels
{
    public class WalletManagementViewModel : ViewModelBase
    {

        private readonly List<OwnedCurrencyWrapper> _currencies = new List<OwnedCurrencyWrapper>();
        public List<OwnedCurrencyWrapper> Currencies { get { return _currencies; } }

        public WalletManagementViewModel()
        {
            CurrencyDataAccess cda = new CurrencyDataAccess();
            foreach (var item in cda.GetCurrencies())
            {
                Currencies.Add(new OwnedCurrencyWrapper(item));
            }

            OnPropertyChanged(nameof(Currencies));
        }

        public class OwnedCurrencyWrapper : ViewModelBase
        {
            public Currency Currency { get; private set; }

            private Boolean _isOwned;
            public Boolean IsOwned
            {
                get { return _isOwned; }
                set {
                    if (_isOwned == value)
                        return;
                    _isOwned = value;
                    Update(); }
            }
            public ICommand Toggled { get; set; }
            

            public OwnedCurrencyWrapper(Currency currency)
            {
                Currency = currency;
                WalletDataAccess wda = WalletDataAccess.Instance;
                IsOwned = wda.IsOwned(Currency.Id);

                //Toggled = new Command(() => Update());
            }

            public void Update()
            {
                WalletDataAccess wda = WalletDataAccess.Instance;
                if (IsOwned) wda.OwnCurrency(Currency.Id);
                else wda.RemoveOwnedCurrency(Currency.Id);

            }

            //public void Toggled_handler(object sender, ToggledEventArgs e)
            //{
            //    Update();
            //}
        }
    }
}
