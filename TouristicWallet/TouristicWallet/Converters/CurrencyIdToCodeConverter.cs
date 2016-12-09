using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristicWallet.Data;
using TouristicWallet.Models;
using Xamarin.Forms;

namespace TouristicWallet.Converters
{
    class CurrencyIdToCodeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            CurrencyDataAccess cda = new CurrencyDataAccess();
            if(value is Int32)
            {
                return cda.GetCurrency((int)value).Initials;
            }


            if(value is Wallet)
            {
                int currencyId = ((Wallet)value).CurrencyId;
                return cda.GetCurrency(currencyId).Initials;    
            }

            return "notFound";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
