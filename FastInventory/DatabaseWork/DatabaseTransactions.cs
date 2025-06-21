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
                    conn.CreateTable<Product>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
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

        public static bool AddProduct(Product product)
        {
            try
            {
                using (var conn = new SQLiteConnection(DBPath))
                {
                    conn.Insert(product);
                    Console.WriteLine($"Product {product.Model} added successfully.");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine($"Error adding product {product.Model} to database.");
            }
            return false;
        }

        public static bool UpdateProduct(Product product)  // No longer creating a new product and adding it to the database, just updating the count and other properties of the existing product
        {

            using (var conn = new SQLiteConnection(DBPath))
            {
                try
                {
                    conn.Update(product);
                    Console.WriteLine($"Product {product.Model} updated successfully.");
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine($"Error updating product {product.Model} in database.");
                }
                return false;
            }

        }

        public static bool RemoveProductFromDatabase(Product product)
        {
            try
            {
                using (var conn = new SQLiteConnection(DBPath))
                {
                    var assetToRemove = conn.Table<Product>().Where(name => name.Model == product.Model).FirstOrDefault();
                    if (assetToRemove != null)
                    {
                        conn.Delete(assetToRemove);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing product from database. Product Name: {product.Model}");
                Console.WriteLine(ex.Message);
            }
            return false;
        }
    }
}
