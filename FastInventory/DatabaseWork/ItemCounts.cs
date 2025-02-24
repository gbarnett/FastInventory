using FastInventory.Classes;
using SQLite;

namespace FastInventory.DatabaseWork
{
    class ItemCounts
    {
        public static string DBPath = Path.Combine(FileSystem.AppDataDirectory, "Inventory.db");

        public async static Task<int> GetMC3300StockCount()
        {
            using (var conn = new SQLiteConnection(DBPath))
            {
                var mc3300StockCount = conn.Table<AssetItem>().Count(s => s.Model == "MC3300" && s.InStock == 1);
                conn.Close();
                return mc3300StockCount;
            }
        }

        public async static Task<int> GetZQ630StockCount()
        {
            using (var conn = new SQLiteConnection(DBPath))
            {
                var zq630StockConut = conn.Table<AssetItem>().Count(s => s.Model == "ZQ630" && s.InStock == 1);
                conn.Close();
                return zq630StockConut;
            }
        }

        public async static Task<int> GetHPG6StockCount()
        {
            using (var conn = new SQLiteConnection(DBPath))
            {
                var hpG6StockCount = conn.Table<AssetItem>().Count(s => s.Model == "HP G6" && s.InStock == 1);
                conn.Close();
                return hpG6StockCount;
            }
        }

        public async static Task<int> GetHPG7StockCount()
        {
            using (var conn = new SQLiteConnection(DBPath))
            {
                var hpG7StockCount = conn.Table<AssetItem>().Count(s => s.Model == "HP G7" && s.InStock == 1);
                conn.Close();
                return hpG7StockCount;
            }
        }

        public async static Task<int> GetLenovoStockCount()
        {
            using (var conn = new SQLiteConnection(DBPath))
            {
                var lenovoStockCount = conn.Table<AssetItem>().Count(s => s.Model == "Lenovo" && s.InStock == 1);
                conn.Close();
                return lenovoStockCount;
            }
        }

        public async static Task<int> GetThinClientStockCount()
        {
            using (var conn = new SQLiteConnection(DBPath))
            {
                var TCStockCount = conn.Table<AssetItem>().Count(s => s.Model == "Thin client" && s.InStock == 1);
                conn.Close();
                return TCStockCount;
            }
        }

        public async static Task<int> GetZD620StockCount()
        {
            using (var conn = new SQLiteConnection(DBPath))
            {
                var ZD620StockCount = conn.Table<AssetItem>().Count(s => s.Model == "ZD620" && s.InStock == 1);
                conn.Close();
                return ZD620StockCount;
            }
        }

        public async static Task<int> GetRadioStockCount()
        {
            using (var conn = new SQLiteConnection(DBPath))
            {
                var RadioStockCount = conn.Table<AssetItem>().Count(s => s.Model == "Radio" && s.InStock == 1);
                conn.Close();
                return RadioStockCount;
            }
        }

        public async static Task<int> GetTabletStockCount()
        {
            using (var conn = new SQLiteConnection(DBPath))
            {
                var TabletStockCount = conn.Table<AssetItem>().Count(s => s.Model == "Tablet" && s.InStock == 1);
                conn.Close();
                return TabletStockCount;
            }
        }

        public async static Task<int> GetZukeyStockCount()
        {
            using (var conn = new SQLiteConnection(DBPath))
            {
                var zukey = conn.Table<Zukey>().Where(s => s.ID == 1).FirstOrDefault();
                var zukeyStockCount = zukey.Count;
                conn.Close();
                return zukeyStockCount;
            }
        }
    }
}
