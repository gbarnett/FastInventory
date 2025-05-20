using FastInventory.Classes;
using System.Threading.Tasks;

namespace FastInventory;

public partial class EditPage : ContentPage
{
	public EditPage(Product product)
	{
		InitializeComponent();
        LoadProduct(product);
	}


    public async Task LoadProduct(Product product)
    {
        Name_Text_Box.Text = product.Model;
        Shelf_Text_Box.Text = product.ShelfLabel;
        Quantity_Text_Box.Text = Convert.ToString(product.Count);
    }

    private void Sumbit_Button_Clicked(object sender, EventArgs e)
    {

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