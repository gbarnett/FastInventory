using FastInventory.Classes;
using FastInventory.DatabaseWork;
using System.ComponentModel;
using System.Threading.Tasks;

namespace FastInventory;

public partial class NewPage : ContentPage
{
    public BindingList<Product> AssetList;
    public NewPage()
	{
		InitializeComponent();
        LoadAssets();

    }

    public async Task LoadAssets()
    {
        List<Product> productList = await DatabaseTransactions.GetProductsAsync();
        BindingList<Product> items = new BindingList<Product>(productList);
        AssetList = items;
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

        }
    }
}