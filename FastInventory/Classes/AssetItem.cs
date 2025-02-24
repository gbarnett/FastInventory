using SQLite;

namespace FastInventory.Classes
{
    public class AssetItem
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [Column("Model")]
        public string Model { get; set; }

        [Column("SerialNumber")]
        public string SerialNumber { get; set; }

        [Column("InStock")]
        public int InStock { get; set; }
    }
}
