using FastInventory.Classes;
using FastInventory.DatabaseWork;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Threading.Tasks;

namespace FastInventory
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<Product> AssetList { get; set; } = new();

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
            CheckData();
            LoadAssets();
        }

        public async Task CheckData()
        {
            await DatabaseTransactions.CheckDatabase();
        }

        public async Task LoadAssets()
        {
            List<Product> productList = await DatabaseTransactions.GetProductsAsync();

            // Synchronize AssetList with productList without clearing
            foreach (var product in productList)
            {
                var existing = AssetList.FirstOrDefault(p => p.Model == product.Model);
                if (existing != null)
                {
                    existing.Count = product.Count;
                    existing.ImageSource = product.ImageSource;
                    existing.Threshold = product.Threshold;
                    // Raise property changed if needed
                }
                else
                {
                    AssetList.Add(product);
                }
            }

            // Remove products that are no longer present
            for (int i = AssetList.Count - 1; i >= 0; i--)
            {
                if (!productList.Any(p => p.Model == AssetList[i].Model))
                {
                    AssetList.RemoveAt(i);
                }
            }
        }

        private async void Add_New_Button_Clicked(object sender, EventArgs e)
        {
            
            await Navigation.PushModalAsync(new AddProduct());
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadAssets(); // Refresh the list when returning to this page
        }

        private void Product_Add_Button_Clicked(object sender, EventArgs e)
        {

            Product? product = (sender as Button).CommandParameter as Product;
            if (product != null)
            {
                product.Count++;
                // Increase the count in the database
                DatabaseTransactions.UpdateProduct(product);
            }
            else
            {
                Console.WriteLine("Product is null, cannot increase count.");
                return;
            }

        }

        private async void Product_Remove_Button_Clicked(object sender, EventArgs e)
        {
            var product = (sender as Button).CommandParameter as Product;

            try
            {
                product.Count--;
                // Decrease the count in the database
                DatabaseTransactions.UpdateProduct(product);
                Console.WriteLine($"Product {product.Model} count decreased to {product.Count}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating product {product.Model}: {ex.Message}");
                await DisplayAlert("Error", $"Failed to update product {product.Model}.", "OK");
                return;
            }
        }

        private async void RemoveItemButton_Clicked(object sender, EventArgs e)
        {
            Product? product = (sender as Button).CommandParameter as Product;
            var answer = await DisplayActionSheet("Remove All Items?", "Cancel", null, "Yes", "No");
            if (answer == "Yes" && product != null)
            {
                DatabaseTransactions.RemoveProductFromDatabase(product);
                await LoadAssets(); // Refresh the list after removing
            }
        }

        private async void Product_View_Button_Clicked(object sender, EventArgs e) // Remove later since this button will not be used
        {
        }

        private async void EditButton_Clicked(object sender, EventArgs e)
        {
            Product? product = (sender as Button).CommandParameter as Product;
            if (product != null) 
            { 
                await Navigation.PushModalAsync(new EditPage(product)); 
            }
        }
    }
}
