using CommunityToolkit.Maui.Views;
using MedicineProject.Model;
using CommunityToolkit.Maui.Alerts;


namespace MedicineProject;

public partial class UpdateMedicinePage : Popup
{
    UpdateRecord updateRecord = new UpdateRecord();

    List<CreateTable> SearchMedicine = new List<CreateTable>();
    string _dbPath = DataBaseConnection._dbPath;

    CreateTable sendMedicineDetail = new CreateTable();
    Int64 medicineID;

    public UpdateMedicinePage(CreateTable getMedicineRecord)
	{
		InitializeComponent();
        sendMedicineDetail = getMedicineRecord;
        GetDetails(sendMedicineDetail);
    }

    public void GetDetails(CreateTable getMedicineDetail)
    {
        medicineID = getMedicineDetail.Id;
        MedicineName.Text = getMedicineDetail.MedicineName;
        MedicineUsedFor.Text = getMedicineDetail.MedicineUsedFor;
        Quantity.Text = getMedicineDetail.Quantity.ToString();
        // ExpiryDate.Date = medicineDetail.ExpiryDate;
        // MedicineImage.Text = medicineDetail.MedicineImage;
    }
    private async void Button_Update(object sender, EventArgs e)
    {
        bool receiveFlag;

        CreateTable sendMedicineRecord = new CreateTable()
        {
            Id = medicineID,
            MedicineName = MedicineName.Text,
            MedicineUsedFor = MedicineUsedFor.Text,
            Quantity = Convert.ToInt64(Quantity.Text),
            ExpiryDate = ExpiryDate.Date.ToString("dd-MM-yyyy")
        };


        receiveFlag = updateRecord.updateMedicineRecord(sendMedicineRecord);

        if (receiveFlag)
        {

            // await DisplayAlert("Confirmation Page ", sendMedicineRecord.MedicineName + " updated successfully...", "ok");
            var toast = Toast.Make(sendMedicineRecord.MedicineName + " updated successfully...", CommunityToolkit.Maui.Core.ToastDuration.Long, 30);
            toast.Show();

        }
        else
        {
            // await DisplayAlert("Error Page ", sendMedicineRecord.MedicineName + " not updated", "ok");
            var toast = Toast.Make(sendMedicineRecord.MedicineName + " not updated", CommunityToolkit.Maui.Core.ToastDuration.Long, 30);
            toast.Show();
        }
        //await Navigation.PopPopupAsync();
        Close(true);
    }

    private void PopupPage_BackgroundClicked(object sender, EventArgs e)
    {
      //  DisplayAlert("Warning!", "Update it or Cancel", "ok");

        var toast = Toast.Make("Update it or Cancel", CommunityToolkit.Maui.Core.ToastDuration.Long, 30);
        toast.Show();
    }
    private async void Button_Cancel(object sender, EventArgs e)
    {
        //Navigation.PopPopupAsync();
        Close(false);
        
    }

}