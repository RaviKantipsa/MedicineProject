
using CommunityToolkit.Maui.Views;
using MedicineProject.Model;
using SQLite;


namespace MedicineProject
{
    public partial class MainPage : ContentPage
    {
        private ListView _listView;
        List<CreateTable> SearchMedicine = new List<CreateTable>();
        string _dbPath = DataBaseConnection._dbPath;
        DeleteRecord deleteRecord = new DeleteRecord();
        UpdateRecord updateRecord = new UpdateRecord();

        public MainPage()
        {
            InitializeComponent();
            if (_dbPath != null)
                GetDetails();
        }
        public void GetDetails()
        {
               var dbConn = new SQLiteConnection(_dbPath);
            
                _listView = new ListView();
                try
                {

                    _listView.ItemsSource = dbConn.Table<CreateTable>().OrderByDescending(x => x.Id).ToList();
                }
                catch (Exception)
                {
                    dbConn.CreateTable<CreateTable>();
                    SearchDataStatus.IsVisible = true;
                    SearchDataStatus.TextColor = Colors.Green;
                    SearchDataStatus.FontSize = 20;
                    SearchDataStatus.Text = "Add some medicine";
                }
            
            MedicineDetails_ListView.ItemsSource = null;

            try
            {
                MedicineDetails_ListView.ItemsSource = _listView.ItemsSource;
               //if (MedicineDetails_ListView.ItemsSource)
               // {
               //     SearchDataStatus.IsVisible = true;
               //     SearchDataStatus.TextColor = Colors.Green;
               //     SearchDataStatus.FontSize = 20;
               //     SearchDataStatus.Text = "Add some medicine";
               // }
            }
            catch (Exception)
            {
                SearchDataStatus.IsVisible = true;
                SearchDataStatus.TextColor = Colors.Red;
                SearchDataStatus.Text = "No Medicine details saved yet..";
            }

        }
        private async void Button_AddMedicine(object sender, EventArgs e)
        {
            this.ShowPopup(new AddMedicinePage());
            GetDetails();
        }
        public async void Button_Delete(object sender, EventArgs e)
    {
        bool receiveFlag;
        bool response = await DisplayAlert("Message", "Do you realy want to delete medicine details", "Ok", "cancel");

        if (response)
        {
            var btn = sender as Button;
            CreateTable sendMedicineRecordId = btn.CommandParameter as CreateTable;
            receiveFlag = deleteRecord.DeleteMedicinetRecord(sendMedicineRecordId);

            if (receiveFlag)
            {
                await DisplayAlert("Message", "Record Deleted...", "Ok");
            }
            else
            {
                await DisplayAlert("Message", "Something went wrong...", "Ok");
            }
            GetDetails();
        }


    }
        public void Button_IncreaseQuantity(object sender, EventArgs e)
           {
             var btn = sender as Button;
            CreateTable getMedicineDetails = btn.CommandParameter as CreateTable;
            getMedicineDetails.Quantity = getMedicineDetails.Quantity + 1;
            updateRecord.updateMedicineRecord(getMedicineDetails);
            GetDetails();
            }
        public void Button_DecreaseQuantity(object sender, EventArgs e)
    {
        var btn = sender as Button;
        CreateTable getMedicineDetails = btn.CommandParameter as CreateTable;
        getMedicineDetails.Quantity = getMedicineDetails.Quantity - 1;
        updateRecord.updateMedicineRecord(getMedicineDetails);
        GetDetails();
    }
        public void Button_Update(object sender, EventArgs e)
    {
        var btn = sender as Button;
        CreateTable sendMedicineRecord = btn.CommandParameter as CreateTable;
       // Navigation.PushPopupAsync(new UpdateMedicine(sendMedicineRecord));
        this.ShowPopup(new UpdateMedicinePage(sendMedicineRecord));
        GetDetails();

    }
        private void Search_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.NewTextValue))
        {
            var db = new SQLiteConnection(_dbPath);
            _listView = new ListView();
            SearchMedicine = db.Table<CreateTable>().OrderBy(x => x.MedicineName).ToList();

            MedicineDetails_ListView.ItemsSource = null;
            MedicineDetails_ListView.ItemsSource = _listView.ItemsSource;

            //List<TemporaryMedicineRecord> data = SearchMedicine.Where(x => x.MedicineName.ToLower().Contains(e.NewTextValue.ToLower())).ToList();

            List<CreateTable> data = SearchMedicine.Where(x => x.MedicineName.ToLower().Contains(e.NewTextValue.ToLower())).ToList();

            MedicineDetails_ListView.ItemsSource = data;
            if (data.Count() == 0)
            {

                SearchDataStatus.IsVisible = true;
               // SearchDataStatus.TextColor = Color.Red;
                SearchDataStatus.Text = "No record found...";
            }
            else
            {
                SearchDataStatus.IsVisible = false;

                SearchDataStatus.IsVisible = true;
                SearchDataStatus.Text = "Total " + data.Count() + " record found";
                //SearchDataStatus.TextColor = Color.Green;
            }
        }
        else
        {
            SearchDataStatus.IsVisible = false;
            GetDetails();

        }
    }
        async void medicineRefreshView_Refreshing(object sender, EventArgs e)
    {
        await Task.Delay(3000);

    }

    }

}
