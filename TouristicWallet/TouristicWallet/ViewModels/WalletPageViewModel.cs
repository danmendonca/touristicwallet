﻿using System;
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
            WalletDataAccess wda = WalletDataAccess.Instance;
            Wallet = wda.GetOwned().ToList(); 
            DefaultText = "Hello";

            this.GoToManagement = new Command ( async () => await Navigation.PushAsync(new Views.WalletManagementPage()) );
        }

    }
}
