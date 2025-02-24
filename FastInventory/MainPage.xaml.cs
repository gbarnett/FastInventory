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
                GetStockCounts();

            }
            catch (Exception ex)
            {
                DisplayAlert("ERROR", ex.Message, "OK");
            }
        }

        private async void MC3300_Add_Button_Clicked(object sender, EventArgs e)
        {
            string serialNumber = await DisplayPromptAsync("Add Scanner", "Enter Scanner Serial Number");
            string model = "MC3300";
            if (!String.IsNullOrEmpty(serialNumber))
            {
                AddAsset(model, serialNumber);
            }
        }

        private async void ZQ630_Add_Button_Clicked(object sender, EventArgs e)
        {
            string serialNumber = await DisplayPromptAsync("Add Printer", "Enter Printer Serial Number");
            string model = "ZQ630";
            if (!String.IsNullOrEmpty(serialNumber))
            {
                AddAsset(model, serialNumber);
            }

        }

        private async void Add_HPG6_Button_Clicked(object sender, EventArgs e)
        {
            string serialNumber = await DisplayPromptAsync("Add Laptop", "Enter Laptop Serial Number");
            string model = "HP G6";
            if (!String.IsNullOrEmpty(serialNumber))
            {
                AddAsset(model, serialNumber);
            }
        }

        private async void Add_HPG7_Button_Clicked(object sender, EventArgs e)
        {
            string serialNumber = await DisplayPromptAsync("Add Laptop", "Enter Laptop Serial Number");
            string model = "HP G7";
            if (!String.IsNullOrEmpty(serialNumber))
            {
                AddAsset(model, serialNumber);
            }
        }

        private async void Add_Lenovo_Button_Clicked(object sender, EventArgs e)
        {
            string serialNumber = await DisplayPromptAsync("Add Laptop", "Enter Laptop Serial Number");
            string model = "Lenovo";
            if (!String.IsNullOrEmpty(serialNumber))
            {
                AddAsset(model, serialNumber);
            }
        }

        private async void Add_TC_Button_Clicked(object sender, EventArgs e)
        {
            string serialNumber = await DisplayPromptAsync("Add Thin Client", "Enter Thin Client Serial Number");
            string model = "Thin client";
            if (!String.IsNullOrEmpty(serialNumber))
            {
                AddAsset(model, serialNumber);
            }
        }

        private async void Add_ZD620_Button_Clicked(object sender, EventArgs e)
        {
            string serialNumber = await DisplayPromptAsync("Add Printer", "Enter Printer Serial Number");
            string model = "ZD620";
            if (!String.IsNullOrEmpty(serialNumber))
            {
                AddAsset(model, serialNumber);
            }
        }

        private void Add_SecurityKey_Button_Clicked(object sender, EventArgs e)
        {
            DatabaseTransactions.AddSecurityKey();
            GetStockCounts();
        }

        private async void Add_Radio_Button_Clicked(object sender, EventArgs e)
        {
            string serialNumber = await DisplayPromptAsync("Add Radio", "Enter Radio Serial Number", "OK", "Cancel", null, 100, Keyboard.Default, "");
            string model = "Radio";
            if (!String.IsNullOrEmpty(serialNumber))
            {
                AddAsset(model, serialNumber);
            }
        }

        private async void Add_Tablet_Button_Clicked(object sender, EventArgs e)
        {
            string serialNumber = await DisplayPromptAsync("Add Tablet", "Enter Tablet Serial Number");
            string model = "Tablet";
            if (!String.IsNullOrEmpty(serialNumber))
            {
                AddAsset(model, serialNumber);
            }
        }

        public async void AddAsset(string model, string serialNumber)
        {
            AssetItem asset = new AssetItem()
            {
                Model = model,
                SerialNumber = serialNumber,
                InStock = 1
            };
            var assetExists = await DatabaseTransactions.CheckAssetExists(serialNumber);
            if (assetExists == null)
            {
                await DatabaseTransactions.AddAsset(asset);
            }
            else
            {
                await DisplayAlert("Error", "Asset already exists", "OK");
            }
        }

        public async void RemoveAsset(string serialNumber)
        {
            var assetExists = await DatabaseTransactions.CheckAssetExists(serialNumber);
            if (assetExists != null)
            {
                await DatabaseTransactions.RemoveAsset(assetExists);
            }
            else
            {
                await DisplayAlert("Error", "Asset does not exist", "OK");
            }
        }

        private async void Remove_MC3300_Button_Clicked(object sender, EventArgs e)
        {
            string serialNumber = await DisplayPromptAsync("Remove Scanner", "Enter Scanner Serial Number");
            if (!String.IsNullOrEmpty(serialNumber))
            {
                RemoveAsset(serialNumber);
                int count = await ItemCounts.GetMC3300StockCount();
                MC3300Label.Text = count.ToString();
            }
        }

        private async void Remove_ZQ630_Button_Clicked(object sender, EventArgs e)
        {
            string serialNumber = await DisplayPromptAsync("Remove Printer", "Enter Printer Serial Number");
            if (!String.IsNullOrEmpty(serialNumber))
            {
                RemoveAsset(serialNumber);
                int count = await ItemCounts.GetZQ630StockCount();
                ZQ630Label.Text = count.ToString();
            }
        }

        private async void Remove_G6_Button_Clicked(object sender, EventArgs e)
        {
            string serialNumber = await DisplayPromptAsync("Remove Laptop", "Enter Laptop Serial Number");
            if (!String.IsNullOrEmpty(serialNumber))
            {
                RemoveAsset(serialNumber);
                int count = await ItemCounts.GetHPG6StockCount();
                HPG6Label.Text = count.ToString();
            }
        }

        private async void Remove_G7_Button_Clicked(object sender, EventArgs e)
        {
            string serialNumber = await DisplayPromptAsync("Remove Laptop", "Enter Laptop Serial Number");
            if (!String.IsNullOrEmpty(serialNumber))
            {
                RemoveAsset(serialNumber);
                int count = await ItemCounts.GetHPG7StockCount();
                HPG7Label.Text = count.ToString();
            }
        }

        private async void Remove_Lenovo_Button_Clicked(object sender, EventArgs e)
        {
            string serialNumber = await DisplayPromptAsync("Remove Laptop", "Enter Laptop Serial Number");
            if (!String.IsNullOrEmpty(serialNumber))
            {
                RemoveAsset(serialNumber);
                int count = await ItemCounts.GetLenovoStockCount();
                LenovoLabel.Text = count.ToString();
            }
        }

        private async void Remove_TC_Button_Clicked(object sender, EventArgs e)
        {
            string serialNumber = await DisplayPromptAsync("Remove Thin Client", "Enter Thin Client Serial Number");
            if (!String.IsNullOrEmpty(serialNumber))
            {
                RemoveAsset(serialNumber);
                int count = await ItemCounts.GetThinClientStockCount();
                TCLabel.Text = count.ToString();
            }
        }

        private async void Remove_ZD620_Button_Clicked(object sender, EventArgs e)
        {
            string serialNumber = await DisplayPromptAsync("Remove Printer", "Enter Printer Serial Number");
            if (!String.IsNullOrEmpty(serialNumber))
            {
                RemoveAsset(serialNumber);
                int count = await ItemCounts.GetZD620StockCount();
                ZD620Label.Text = count.ToString();
            }
        }

        private async void Remove_Radio_Button_Clicked(object sender, EventArgs e)
        {
            string serialNumber = await DisplayPromptAsync("Remove Radio", "Enter Radio Serial Number");
            if (!String.IsNullOrEmpty(serialNumber))
            {
                RemoveAsset(serialNumber);
                int count = await ItemCounts.GetTabletStockCount();
                RadioLabel.Text = count.ToString();
            }
        }

        private async void Remove_Tablet_Button_Clicked(object sender, EventArgs e)
        {
            string serialNumber = await DisplayPromptAsync("Remove Tablet", "Enter Tablet Serial Number");
            if (!String.IsNullOrEmpty(serialNumber))
            {
                RemoveAsset(serialNumber);
                int count = await ItemCounts.GetTabletStockCount();
                TabletLabel.Text = count.ToString();
            }
        }

        private async void GetStockCounts()
        {
            int mc3300StockCount = await ItemCounts.GetMC3300StockCount();
            int zq630StockCount = await ItemCounts.GetZQ630StockCount();
            int hpG6StockCount = await ItemCounts.GetHPG6StockCount();
            int hpG7StockCount = await ItemCounts.GetHPG7StockCount();
            int lenovoStockCount = await ItemCounts.GetLenovoStockCount();
            int zd620StockCount = await ItemCounts.GetZD620StockCount();
            int tcStockCount = await ItemCounts.GetThinClientStockCount();
            int radioStockCount = await ItemCounts.GetRadioStockCount();
            int tabletStockCount = await ItemCounts.GetTabletStockCount();
            int zukeyStockCount = await ItemCounts.GetZukeyStockCount();

            MC3300Label.Text = mc3300StockCount.ToString();
            ZQ630Label.Text = zq630StockCount.ToString();
            HPG6Label.Text = hpG6StockCount.ToString();
            HPG7Label.Text = hpG7StockCount.ToString();
            LenovoLabel.Text = lenovoStockCount.ToString();
            ZD620Label.Text = zd620StockCount.ToString();
            TCLabel.Text = tcStockCount.ToString();
            RadioLabel.Text = radioStockCount.ToString();
            TabletLabel.Text = tabletStockCount.ToString();
            ZukeyLabel.Text = zukeyStockCount.ToString();
        }

        private async void Remove_Zukey_Button_Clicked(object sender, EventArgs e)
        {
            DatabaseTransactions.RemoveSecurityKey();
            int count = await ItemCounts.GetZukeyStockCount();
            ZukeyLabel.Text = count.ToString();
        }
    }
}
