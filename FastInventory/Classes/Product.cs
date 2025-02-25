using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastInventory.Classes
{
    public class Product
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [Column("Model")]
        public string Model { get; set; }

        [Column("IsAsset")]
        public int IsAsset { get; set; }

        [Column("ImageSource")]
        public string ImageSource { get; set; }

        [Column("Count")]
        public int Count { get; set; }

        public Product()
        {

        }
    }
}
