using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristicWallet.models;
using Xamarin.Forms;

namespace TouristicWallet.Data
{
    class CurrencyDataAccess
    {
        private SQLiteConnection database;
        private static object collisionLock = new object();

        public ObservableCollection<Currency> Currencies { get; set; }

        public CurrencyDataAccess()
        {
            database = DependencyService.Get<IDatabaseConnection>().DbConnection();
            database.CreateTable<Currency>();
            this.Currencies = new ObservableCollection<Currency>(database.Table<Currency>());
        }

        public IEnumerable<Currency> GetCurrencies()
        {
            lock (collisionLock)
            {
                var query = from curr in database.Table<Currency>()
                            select curr;
                return query.AsEnumerable();
            }
        }

        public Currency GetCurrency(string initials)
        {
            lock (collisionLock)
            {
                return database.Table<Currency>().
                  FirstOrDefault(currency => currency.Initials.Equals(initials));
            }
        }

        public int SaveCurrency(Currency currency)
        {
            lock (collisionLock)
            {
                if (currency.Id != 0)
                {
                    database.Update(currency);
                    return currency.Id;
                }
                else
                {
                    database.Insert(currency);
                    return currency.Id;
                }
            }
        }

        public void SaveAllCurrencies()
        {
            lock (collisionLock)
            {
                foreach (var currency in this.Currencies)
                {
                    if (currency.Id != 0)
                    {
                        database.Update(currency);
                    }
                    else
                    {
                        database.Insert(currency);
                    }
                }
            }
        }
    }
}
