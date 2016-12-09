using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristicWallet.Models
{
    [Table("Wallet")]
    public class Wallet
    {
        private Currency _currency;

        private Int32 _currencyId;

        [PrimaryKey]
        public Int32 CurrencyId
        {
            get { return _currencyId; }
            set { _currencyId = value;}
        }

        private double _amount;

        [NotNull]
        public double Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public Currency getCurrency()
        {
            throw new NotImplementedException();
        }

    }
}
