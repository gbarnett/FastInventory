using SQLite;

namespace FastInventory.Classes
{
    class Zukey
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [Column("Count")]
        public int Count { get; set; }
    }
}
