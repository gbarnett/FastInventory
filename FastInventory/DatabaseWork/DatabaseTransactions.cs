using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using FastInventory.Classes;
using SQLite;

namespace FastInventory.DatabaseWork
{

    // C:\Users\Gary\AppData\Local\User Name\com.companyname.fastinventory\Data
    internal class DatabaseTransactions
    {
        public static string DBPath = Path.Combine(FileSystem.AppDataDirectory, "Inventory.db");
        public async static Task CheckDatabase()
        {
            using (SQLiteConnection conn = new SQLiteConnection(DBPath))
            {
                try
                {
                    conn.CreateTable<AssetItem>();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public async static Task AddAsset(AssetItem asset)
        {
            using (SQLiteConnection conn = new SQLiteConnection(DBPath))
            {
                conn.Insert(asset);
                conn.Close();
            }
        }

        public async static Task<AssetItem> CheckAssetExists(string serialNumber)
        {
            using (var conn = new SQLiteConnection(DBPath))
            {
                var asset = conn.Table<AssetItem>().Where(s => s.SerialNumber == serialNumber && s.InStock == 1).FirstOrDefault();
                conn.Close();
                return await Task.FromResult(asset);
            }
        }
    }
}
