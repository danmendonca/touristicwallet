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
    /// <summary>
    /// 
    /// </summary>
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
        }

        public IEnumerable<Wallet> Wallet { get; set; }

        private WalletDataAccess()
        {
            lock (_collisionLock)
            {
                _database = DependencyService.Get<IDatabaseConnection>().DbConnection();
                _database.CreateTable<Wallet>();
                Wallet = _database.Table<Wallet>().ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Wallet> GetOwned()
        {
            return Wallet;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Boolean IsOwned(Int32 id)
        {
            return _database.Table<Wallet>().FirstOrDefault(w => w.CurrencyId == id) != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public void OwnCurrency(Int32 id)
        {
            new Wallet(id);
            lock (_collisionLock)
            {
                if(_database.Table<Wallet>().FirstOrDefault(w => w.CurrencyId == id) == null)
                    _database.Insert(new Wallet(id));
            }
            UpdateWallet();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Boolean RemoveOwnedCurrency(Int32 id)
        {
            lock (_collisionLock)
            {
                if (_database.Table<Wallet>().Delete(w => w.CurrencyId == id) != 1) return false;

                UpdateWallet();
                return true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="w"></param>
        /// <returns></returns>
        public Wallet Update(Wallet w)
        {

            lock (_collisionLock)
            {
                _database.Update(w);
                UpdateWallet();
            }

            return w;
        }


        public IEnumerable<Wallet> UpdateWallet()
        {
            Wallet = _database.Table<Wallet>().ToList();
            return Wallet;
        }
    }
}
