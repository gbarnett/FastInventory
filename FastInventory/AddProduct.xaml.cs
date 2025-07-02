using FastInventory.Classes;
using FastInventory.DatabaseWork;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace FastInventory;

public partial class AddProduct : ContentPage
{

    public ObservableCollection<ImageOption> ImageOptions { get; set; } = new ObservableCollection<ImageOption>();

    public string _selectedImage = "default.png";
    public AddProduct()
	{
		InitializeComponent();
        LoadImageOptions();
        BindingContext = this;
	}

    private void LoadImageOptions()
    {
        ImageOptions.Clear();
        var imageNames = new List<string> { "adapter.png" , "battery.png" , "display.png" , "displayport.png", 
            "ethernet.png" ,"handscanner.png" , "hdmi.png" , "headphones.png" , "key.png" , 
            "monitor.png" , "usbcable.png", "usbdrive.png"};
        foreach (var name in imageNames)
        {
            ImageOptions.Add(new ImageOption { ImageName = name, isSelected = false });
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
        product.ImageSource = _selectedImage;
        product.ShelfLabel = ShelfLabel.Text;
        if (ShelfLabel.Text.Length > 10)
        {
            await DisplayAlert("Error", "Shelf Label Must not Exceed 10 charcters","OK");
            return;
        }
        try
        {
            product.Threshold = int.Parse(Threshold.Text);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error","Threshold Must be a number","OK");
            Console.WriteLine(ex.Message);
            return;
        }
        DatabaseTransactions.AddProduct(product);
        await Navigation.PopModalAsync();

    }

    private void ImageOptionsView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is ImageOption selected)
        {
            _selectedImage = selected.ImageName;
            
        }
    }
}