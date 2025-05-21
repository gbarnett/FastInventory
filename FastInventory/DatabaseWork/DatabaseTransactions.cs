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
                    conn.CreateTable<Product>();
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


        public async static Task RemoveSerializedAsset(string serialNumber)
        {
            using (var conn = new SQLiteConnection(DBPath))
            {
                var assetToRemove = conn.Table<AssetItem>().Where(s => s.SerialNumber == serialNumber).FirstOrDefault();
                if (assetToRemove != null)
                {
                    conn.Delete(assetToRemove);
                }
            }
        }

        public async static Task RemoveNonSerializedAsset(string model)
        {
            using (var conn = new SQLiteConnection(DBPath))
            {
                var assetsToRemove = conn.Table<AssetItem>().Where(s => s.Model == model && s.SerialNumber == null).ToList();
                if(assetsToRemove.Count > 0)
                {
                    conn.Delete(assetsToRemove[0]);
                }
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

        public static async Task<List<Product>> GetProductsAsync()
        {
            var conn = new SQLiteAsyncConnection(DBPath);
            var products = await conn.Table<Product>().ToListAsync();
            await conn.CloseAsync();
            return products;
        }

        public static async Task AddProduct(Product product)
        {
            using (var conn = new SQLiteConnection(DBPath))
            {
                conn.Insert(product);
            }
        }

        public static async Task UpdateProductName(Product product, string newName, string oldName)
        {
            using (var conn = new SQLiteConnection(DBPath))
            {
                var list = conn.Table<Product>().Where(p => p.Model == oldName).ToList();
                foreach (var item in list)
                {
                    item.Model = newName;
                    conn.Update(item);
                }
            }
        }

        public static async Task UpdateProduct(Product product)  // FIX HOW THE DB STORES PRODUCT COUNTS AND HOW THE PROGRAM ACTUALLY STORES COUNTS.  CURRENTLY IT STORES COUNT IN A COUNT ATTRIBUUTE HOWEVER SHOULD BE THE TOTAL NUMBER OF ASSETS AND PRODUCTS IN THE DB
        {
            var count = await ItemCounts.GetProductCount(product.Model);
            if (count < product.Count)
            {
                var difference = product.Count - count;
                for (int x = 0; x < difference; x++)
                {
                    await AddProduct(product);
                }
                using (var conn = new SQLiteConnection(DBPath))
                {
                    product.Count = difference;
                    conn.Update(product);
                }
            }
            if (count > product.Count)
            {
                var differnce = count - product.Count;
                for (int x = 0; x < differnce; x++)
                {
                    await RemoveNonSerializedAsset(product.Model);
                }
                using (var conn = new SQLiteConnection(DBPath))
                {
                    conn.Update(product);
                }
            }
            else
            {
                using (var conn = new SQLiteConnection(DBPath))
                {
                    conn.Update(product);
                }
            }
            
        }
        public async static Task RemoveFirstAssetByName(string model)
        {
            using (var conn = new SQLiteConnection(DBPath))
            {
                var assetToRemove = conn.Table<AssetItem>().Where(s => s.Model == model).FirstOrDefault();
                if (assetToRemove != null)
                {
                    conn.Delete(assetToRemove);
                }
            }
        }

        public async static Task RemoveProductFromDatabase(Product product)
        {
            using (var conn = new SQLiteConnection(DBPath))
            {
                var assetsToRemove = conn.Table<AssetItem>().Where(s => s.Model == product.Model).ToList();
                foreach (var asset in assetsToRemove)
                {
                    conn.Delete(asset);
                }
                conn.Delete(product);
            }
        }

        public async static Task<List<AssetItem>> GetSpecificProductList(Product product)
        {
            List<AssetItem> items = new List<AssetItem>();
            using (var conn = new SQLiteConnection(DBPath))
            {
                items = conn.Table<AssetItem>().Where(p => p.Model == product.Model).ToList();
            }
                return items;
        }
    }
}
