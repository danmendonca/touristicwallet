﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace TouristicWallet.Views
{
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();
            Master = new Master();
            Detail = new NavigationPage(new WalletPage());

            App.MasterDetail = this;
        }
    }
}
