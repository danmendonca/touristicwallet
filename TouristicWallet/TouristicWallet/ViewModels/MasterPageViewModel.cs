using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TouristicWallet.Data;
using TouristicWallet.Models;
using TouristicWallet.Views;
using Xamarin.Forms;

namespace TouristicWallet.ViewModels
{
    public class MasterPageViewModel : ViewModelBase
    {
        public ICommand GoToManagement { get; set; }
        public ICommand GoToConversion { get; set; }

        private List<String> _wallet;

        public List<String> Wallet
        {
            get { return _wallet; }
            set { _wallet = value; }
        }

        private Picker _picker = new Picker();

        public Picker Picker
        {
            get { return _picker; }
            set { _picker = value; }
        }
        


        public MasterPageViewModel()
        {

            Update();

            GoToManagement = new Command(async () =>
           {
               await App.NavigateMasterDetail(new WalletManagementPage());
           });

            GoToConversion = new Command(async () =>
            {
                String currency = Picker.Items[Picker.SelectedIndex];
                await App.NavigateMasterDetail(new ConvertPage(currency));
            });

        }


        public void Update()
        {

            Picker.Items.Clear();
            foreach (var item in WalletDataAccess.Instance.GetOwned().ToList())
            {
                Picker.Items.Add(item.Currency.Initials);
            }
            if (Picker.Items.Count > 0)
            {
                Picker.SelectedIndex = 0;
            }

            //OnPropertyChanged(nameof(Picker));
        }
    }
}
