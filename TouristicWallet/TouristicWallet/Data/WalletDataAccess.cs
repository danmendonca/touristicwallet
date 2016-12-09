using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristicWallet.Models;
using Xamarin.Forms;

namespace TouristicWallet.Data
{
    public class WalletDataAccess
    {
        private static SQLiteConnection _database;
        private static object _collisionLock = new object();

        private static WalletDataAccess _instance;
        public static WalletDataAccess Instance
        {
            get
            {
                if (_instance == null) _instance = new WalletDataAccess();
                return _instance;
            }
            private set { }
        }

        public ObservableCollection<Wallet> Wallet { get; set; }

        private WalletDataAccess()
        {
            lock (_collisionLock)
            {
                _database = DependencyService.Get<IDatabaseConnection>().DbConnection();
                _database.CreateTable<Wallet>();
                Wallet = new ObservableCollection<Models.Wallet>();
            }
        }

        public IEnumerable<Wallet> GetOwned()
        {
            return _database.Table<Wallet>().AsEnumerable();
        }

        public Boolean IsOwned(Int32 id)
        {
            return _database.Table<Wallet>().FirstOrDefault(w => w.CurrencyId == id) != null;
        }

        public Boolean OwnCurrency(Int32 id)
        {
            if (!IsOwned(id))
            {
                lock (_collisionLock)
                {
                    return _database.Insert(new Wallet(id)) == 1;
                }
            }

            return false;
        }

        public Boolean RemoveOwnedCurrency(Int32 id)
        {
            if(IsOwned(id))
            {
                lock(_collisionLock)
                {
                    return _database.Table<Wallet>().Delete(w => w.CurrencyId == id) == 1;
                }
            }
            return false;
        }

        public Wallet Update(Wallet w)
        {

            lock(_collisionLock)
            {
                _database.Update(w);
            }

            return w;
        }
    }
}
