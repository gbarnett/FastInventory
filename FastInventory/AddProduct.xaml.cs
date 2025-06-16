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
        var imageNames = new List<string> { "ethernet.png", "radio.png", "scanning.png", "computer.png", "laptop.png", "monitor.png", "usbcable.png", "display.png", "key.png", "tablet.png", "printer.png"};
        foreach (var name in imageNames)
        {
            ImageOptions.Add(new ImageOption { ImageName = name, isSelected = false });
        }
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
            //FileName.Text = filepath;
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
        product.ImageSource = _selectedImage;
        product.ShelfLabel = ShelfLabel.Text;
        try
        {
            product.Threshold = int.Parse(Threshold.Text);
        }
        catch (Exception ex)
        {
            DisplayAlert("Error","Threshold Must be a number","OK");
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