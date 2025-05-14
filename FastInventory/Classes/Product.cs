using FastInventory.DatabaseWork;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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

        [Column("Threshold")]
        public int Threshold { get; set; }

        public Color BackgroundColor => Count <= Threshold ? Colors.Red : Colors.Transparent;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public Product()
        {
        }
    }
}
