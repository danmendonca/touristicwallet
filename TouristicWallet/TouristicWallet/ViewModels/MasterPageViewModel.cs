using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TouristicWallet.Views;
using Xamarin.Forms;

namespace TouristicWallet.ViewModels
{
    public class MasterPageViewModel : ViewModelBase
    {
        public ICommand GoToManagement { get; set; }

        public MasterPageViewModel()
        {
            GoToManagement = new Command(async () =>
           {
               await App.NavigateMasterDetail(new WalletManagementPage());
           });
        }
    }
}
