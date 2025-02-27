using FastInventory.Classes;
using FastInventory.DatabaseWork;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FastInventory;

public partial class NewPage : ContentPage
{
    public ObservableCollection<Product> AssetList { get; set; } = new();

    public NewPage()
	{
		InitializeComponent();
        LoadAssets();
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
        foreach(var item in AssetList)
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
}