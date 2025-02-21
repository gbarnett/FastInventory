using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace FastInventory
{
    internal class DatabaseTransactions
    {
        public static string DBPath = Path.Combine(FileSystem.AppDataDirectory, "Inventory.db");
        public async static void CheckDatabase()
        {
            using (SQLiteConnection conn = new SQLiteConnection(DBPath))
            {
                try
                {
                    conn.CreateTable<MC3000>();
                    conn.Close();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
