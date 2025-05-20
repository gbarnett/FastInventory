using FastInventory.DatabaseWork;
using SQLite;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Graphics; // Ensure this namespace is used for Color

namespace FastInventory.Classes
{
    public class Product : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [Column("Model")]
        public string Model { get; set; }

        [Column("IsAsset")]
        public int IsAsset { get; set; }

        [Column("ImageSource")]
        public string ImageSource { get; set; }

        [Column("Threshold")]
        public int Threshold { get; set; }

        [Column("ShelfLabel")]
        public string ShelfLabel { get; set; }

        private int count;
        [Column("Count")]
        public int Count
        {
            get => count;
            set
            {
                if (count != value)
                {
                    count = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(BackgroundColor)); // update UI when color condition changes
                }
            }
        }

        public Color BackgroundColor => Count <= Threshold ? Colors.Red : Colors.Transparent;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public Product()
        {
        }
    }
}
