using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace FastInventory
{
    public class MC3000
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [Column("Model")]
        public string Model { get; set; }

        [Column("SerialNumber")]
        public string SerialNumber { get; set; }

        public MC3000()
        {

        }

    }
}
