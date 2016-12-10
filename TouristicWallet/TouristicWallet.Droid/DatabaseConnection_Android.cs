using SQLite;
using LocalDataAccess.Droid;
using System.IO;
using TouristicWallet;

[assembly: Xamarin.Forms.Dependency(typeof(DatabaseConnection_Android))]
namespace LocalDataAccess.Droid
{
    public class DatabaseConnection_Android : TouristicWallet.Data.IDatabaseConnection
    {
        public SQLiteConnection DbConnection()
        {
            var dbName = "touristicwallet.db3";
            var path = Path.Combine(System.Environment.
              GetFolderPath(System.Environment.
              SpecialFolder.Personal), dbName);
            return new SQLiteConnection(path);
        }
    }
}