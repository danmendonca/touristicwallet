using System;
using SQLite;

namespace TouristicWallet
{
    public interface IDatabaseConnection
    {
        SQLiteConnection DbConnection();
    }
}
