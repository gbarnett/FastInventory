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
                    conn.CreateTable<Zukey>();
                    if (conn.Table<Zukey>().Count() == 0)
                    {
                        conn.Insert(new Zukey() { Count = 0 });
                    }
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

        public async static Task RemoveAsset(AssetItem asset)
        {
            using (SQLiteConnection conn = new SQLiteConnection(DBPath))
            {
                conn.Delete(asset);
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

        public async static Task AddSecurityKey()
        {
            using (var conn = new SQLiteConnection(DBPath))
            {
                var zukey = conn.Table<Zukey>().Where(s => s.ID == 1).FirstOrDefault();
                zukey.Count += 1;
                conn.Update(zukey);
                conn.Close();
            }
        }

        public async static Task RemoveSecurityKey()
        {
            using (var conn = new SQLiteConnection(DBPath))
            {
                var zukey = conn.Table<Zukey>().Where(s => s.ID == 1).FirstOrDefault();
                if (zukey.Count > 0)
                {
                    zukey.Count -= 1;
                    conn.Update(zukey);
                }
                else
                {
                    return;
                }

            }
        }

    }
}
