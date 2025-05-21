using FastInventory.Classes;
using FastInventory.DatabaseWork;
using System.Threading.Tasks;

namespace FastInventory;

public partial class EditPage : ContentPage
{

    Product productToEdit = new Product();
    public EditPage(Product product)
	{
		InitializeComponent();
        LoadProduct(product);
        productToEdit = product;
    }

    public async Task LoadProduct(Product product)
    {
        Name_Text_Box.Text = product.Model;
        Shelf_Text_Box.Text = product.ShelfLabel;
        Quantity_Text_Box.Text = Convert.ToString(product.Count);
        if (product.IsAsset == 1)
        {
            Quantity_Text_Box.IsEnabled = false;
        }
    }

    private async void Sumbit_Button_Clicked(object sender, EventArgs e)
    {

        string newName = Convert.ToString(Name_Text_Box.Text);

        if (productToEdit.Model != newName)
        {
            await DatabaseTransactions.UpdateProductName(productToEdit, newName, productToEdit.Model);
        }


        productToEdit.Model = Name_Text_Box.Text;
        productToEdit.ShelfLabel = Shelf_Text_Box.Text;
        try
        {
            productToEdit.Count = Convert.ToInt32(Quantity_Text_Box.Text);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "Please enter a valid number for quantity.", "OK");
            return;
        }

        await DatabaseTransactions.UpdateProduct(productToEdit);
        await DatabaseTransactions.GetSpecificProductList(productToEdit);
        Go_Back_Home();

    }

    private void Cancel_Button_Clicked(object sender, EventArgs e)
    {
        Go_Back_Home();
    }

    public async Task Go_Back_Home()
    {
        await Navigation.PopModalAsync();
    }
}