using System;
using SQLite;

namespace TouristicWallet.Data
{
    public interface IDatabaseConnection
    {
        SQLiteConnection DbConnection();
    }
}
