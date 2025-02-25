using FastInventory.Classes;
using SQLite;
using System.ComponentModel;

namespace FastInventory.DatabaseWork
{

    // C:\Users\Gary\AppData\Local\User Name\com.companyname.fastinventory\Data
    internal class DatabaseTransactions
    {
        public static string DBPath = Path.Combine(FileSystem.AppDataDirectory, "Inventory.db");
        public async static Task CheckDatabase()
        {
            using (var conn = new SQLiteConnection(DBPath))
            {
                try
                {
                    conn.CreateTable<AssetItem>();
                    conn.CreateTable<Zukey>();
                    conn.CreateTable<Product>();
                    if (conn.Table<Zukey>().Count() == 0)
                    {
                        conn.Insert(new Zukey() { Count = 0 });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public async static Task AddAsset(AssetItem asset)
        {
            using (var conn = new SQLiteConnection(DBPath))
            {
                conn.Insert(asset);
            }
        }

        public async static Task RemoveAsset(AssetItem asset)
        {
            using (var conn = new SQLiteConnection(DBPath))
            {
                conn.Delete(asset);
            }
        }

        public async static Task<AssetItem> CheckAssetExists(string serialNumber)
        {
            using (var conn = new SQLiteConnection(DBPath))
            {
                var asset = conn.Table<AssetItem>().Where(s => s.SerialNumber == serialNumber && s.InStock == 1).FirstOrDefault();
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

        public static async Task<List<Product>> GetProductsAsync()
        {
            var conn = new SQLiteAsyncConnection(DBPath);
            var products = await conn.Table<Product>().ToListAsync();
            await conn.CloseAsync();
            return products;
        }

        public static async Task DropTable()
        {
            using (var conn = new SQLiteConnection(DBPath))
            {
                conn.DropTable<Product>();
            }
        }

        public static async Task AddProduct(Product product)
        {
            using (var conn = new SQLiteConnection(DBPath))
            {
                conn.Insert(product);
            }
        }
    }
}
