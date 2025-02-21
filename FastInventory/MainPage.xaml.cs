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
                DisplayAlert("ERROR",ex.Message,"OK");
            }
        }

    }

}
