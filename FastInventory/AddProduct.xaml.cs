using FastInventory.Classes;
using FastInventory.DatabaseWork;
using System.Threading.Tasks;

namespace FastInventory;

public partial class AddProduct : ContentPage
{
	public AddProduct()
	{
		InitializeComponent();
	}

    private async void BrowseButon_Clicked(object sender, EventArgs e)
    {
        var result = await FilePicker.PickAsync(new PickOptions
        {
            FileTypes = FilePickerFileType.Images,
            PickerTitle = "Pick a picture"
        });
        if (result != null)
        {
            string filepath = result.FullPath;
            FileName.Text = filepath;
        }
        
    }

    private void Submit_Clicked(object sender, EventArgs e)
    {
        CreateProduct();
    }

    private async void Cancel_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    public async Task CreateProduct()
    {
        Product product = new Product();
        product.Model = ModelName.Text;
        if (Serialized.IsChecked)
        {
            product.IsAsset = 1;
        }
        else
        {
            product.IsAsset = 0;
        }
        if (!String.IsNullOrEmpty(FileName.Text))
        {
            product.ImageSource = FileName.Text;
        }
        DatabaseTransactions.AddProduct(product);
        await Navigation.PopModalAsync();

    }
}