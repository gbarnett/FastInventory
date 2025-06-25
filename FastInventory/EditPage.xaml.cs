using FastInventory.Classes;
using FastInventory.DatabaseWork;
using System.Collections.ObjectModel;
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

    public void LoadProduct(Product product)
    {
        Name_Text_Box.Text = product.Model;
        Shelf_Text_Box.Text = product.ShelfLabel;
        Quantity_Text_Box.Text = Convert.ToString(product.Count);
    }

    private async void Sumbit_Button_Clicked(object sender, EventArgs e)
    {

        try
        {
            string name = Name_Text_Box.Text;
            string shelf = Shelf_Text_Box.Text;
            int quantity = Quantity_Text_Box.Text == "" ? 0 : Convert.ToInt32(Quantity_Text_Box.Text);
            if (shelf.Length > 10)
            {
                await DisplayAlert("Error", "Shelf Label must not exceed 10 characters.", "OK");
                return;
            }
            productToEdit.Model = name;
            productToEdit.ShelfLabel = shelf;
            productToEdit.Count = quantity;
            DatabaseTransactions.UpdateProduct(productToEdit);
            await Go_Back_Home();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "Please check your input values.", "OK");
            Console.WriteLine(ex.Message);
        }

    }

    private async void Cancel_Button_Clicked(object sender, EventArgs e)
    {
        await Go_Back_Home();
    }

    public async Task Go_Back_Home()
    {
        await Navigation.PopModalAsync();
        if (Application.Current.MainPage is NavigationPage navPage && navPage.CurrentPage is MainPage mainPage)
        {
            await mainPage.LoadAssets();
        }
    }
}