﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace TouristicWallet.Views
{
    public partial class Master : ContentPage
    {
        public Master()
        {
            InitializeComponent();
            ViewModel.Picker = picker;
            //picker2 = picker;
            ViewModel.Update();
        }

        public void Update()
        {
            ViewModel.Update();
        }
    }
}
