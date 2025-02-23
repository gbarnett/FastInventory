using System.Threading.Tasks;
using FastInventory.Classes;
using FastInventory.DatabaseWork;

namespace FastInventory
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            try
            {
                DatabaseTransactions.CheckDatabase();
            }
            catch (Exception ex)
            {
                DisplayAlert("ERROR", ex.Message, "OK");
            }
        }

        private async void MC3300_Add_Button_Clicked(object sender, EventArgs e)
        {
            string serialNumber = await DisplayPromptAsync("Add Scanner", "Enter Scanner Serial Number");
            if (!String.IsNullOrEmpty(serialNumber))
            {
                AssetItem newScanner = new AssetItem()
                {
                    Model = "MC3300",
                    SerialNumber = serialNumber,
                    InStock = 1
                };
                var scanner = await DatabaseTransactions.CheckAssetExists(serialNumber);
                if (scanner == null)
                {
                    await DatabaseTransactions.AddAsset(newScanner);
                }
                else
                {
                    await DisplayAlert("ERROR", "Scanner already exists", "OK");
                    return;
                }
            }
        }

        private async void ZQ630_Add_Button_Clicked(object sender, EventArgs e)
        {
            string serialNumber = await DisplayPromptAsync("Add Printer", "Enter Printer Serial Number");
            if (!String.IsNullOrEmpty(serialNumber))
            {
                AssetItem newPrinter = new AssetItem()
                {
                    Model = "ZQ630",
                    SerialNumber = serialNumber,
                    InStock = 1
                };
                var printer = await DatabaseTransactions.CheckAssetExists(serialNumber);
                if (printer == null)
                {
                    await DatabaseTransactions.AddAsset(newPrinter);
                }
                else
                {
                    await DisplayAlert("Error", "Printer already exists", "OK");
                    return;
                }
            }

        }

    }

}
