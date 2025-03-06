using FastInventory.Classes;
using FastInventory.DatabaseWork;
using System.Collections.ObjectModel;

namespace FastInventory
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<Product> AssetList { get; set; } = new();

        public MainPage()
        {
            InitializeComponent();
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
            AssetList = new ObservableCollection<Product>(productList);
            await GetCounts();
            ItemLists.ItemsSource = AssetList;
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

        private async Task GetCounts()
        {
            foreach (var item in AssetList)
            {
                item.Count = await ItemCounts.GetProductCount(item.Model);
            }
        }

        private async void Product_Add_Button_Clicked(object sender, EventArgs e)
        {
            var product = (sender as Button).CommandParameter as Product;
            if (product.IsAsset == 1)
            {
                string serialNumber = await DisplayPromptAsync("Add Item", "Enter Serial Number", "OK", "Cancel", null, 100, Keyboard.Default, "");
                if (!String.IsNullOrEmpty(serialNumber))
                {
                    AssetItem asset = new AssetItem();
                    asset.SerialNumber = serialNumber;
                    asset.Model = product.Model;
                    asset.InStock = 1;
                    DatabaseTransactions.AddAsset(asset);
                    await LoadAssets();

                }
            }
            else
            {
                AssetItem asset = new AssetItem();
                asset.Model = product.Model;
                asset.InStock = 1;
                DatabaseTransactions.AddAsset(asset);
                await LoadAssets();

            }
        }

        private async void Product_Remove_Button_Clicked(object sender, EventArgs e)
        {
            var product = (sender as Button).CommandParameter as Product;

            if (product.IsAsset == 1)
            {
                string serialNumber = await DisplayPromptAsync("Remove Item", "Enter Serial Number", "OK", "Cancel", null, 100, Keyboard.Default, "");
                if (!String.IsNullOrEmpty(serialNumber))
                {
                    AssetItem asset = new AssetItem();
                    asset.SerialNumber = serialNumber;
                    asset.Model = product.Model;
                    asset.InStock = 1;
                    DatabaseTransactions.RemoveSerializedAsset(asset.SerialNumber);
                    await LoadAssets();
                }
            }
            else
            {
                DatabaseTransactions.RemoveNonSerializedAsset(product.Model);
                await LoadAssets();
            }
        }

        private async void RemoveItemButton_Clicked(object sender, EventArgs e)
        {
            var product = (sender as Button).CommandParameter as Product;
            var answer = await DisplayActionSheet("Remove All Items?", "Cancel", null, "Yes", "No");
            if (answer == "Yes")
            {
                await DatabaseTransactions.RemoveProductFromDatabase(product);
                await LoadAssets();
            }
        }

        private async void Product_View_Button_Clicked(object sender, EventArgs e)
        {
            var product = (sender as Button).CommandParameter as Product;
            var items = await DatabaseTransactions.GetSpecificProductList(product);

            if (product.IsAsset == 0)
            {
                return;
            }

            if (items == null || items.Count == 0)
            {
                await DisplayAlert("No Items", $"No items found for {product.Model}.", "OK");
                return;
            }

            string list = string.Join("\n", items.Select(item => $"{item.Model} - {item.SerialNumber}"));

            await DisplayAlert(product.Model, list, "OK");
        }
    }
}
