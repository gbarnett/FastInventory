using FastInventory.Classes;
using SQLite;

namespace FastInventory.DatabaseWork
{
    class ItemCounts
    {
        public static string DBPath = Path.Combine(FileSystem.AppDataDirectory, "Inventory.db");


        public async static Task<int> GetProductCount(string model)
        {
            var count = 0;
            using (var conn = new SQLiteConnection(DBPath))
            {
                count = conn.Table<AssetItem>().Count(s => s.Model == model);
                return count;
            }
        }
    }
}
