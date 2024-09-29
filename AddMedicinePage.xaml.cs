using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Views;
using MedicineProject.Model;


namespace MedicineProject;


public partial class AddMedicinePage : Popup
{
    InsertRecord insertRecord = new InsertRecord();
    private Image _photoImage;

    public AddMedicinePage()
	{
		InitializeComponent();
	}
    public void TextChanged_MedicineName(object sender, TextChangedEventArgs e)
    {
        Entry fentry = sender as Entry;
        if (string.IsNullOrEmpty(fentry.Text))
        {
            ButtonSave.IsEnabled = false;
        }
        else
        {
            ButtonSave.IsEnabled = true;
            ButtonSave.BackgroundColor = Colors.DodgerBlue;
            ButtonSave.Opacity = 1;
        }

    }
    private async void Button_TakePicture(object sender, EventArgs e)
    {
        var status = await Permissions.RequestAsync<Permissions.Camera>();
        if (status == PermissionStatus.Granted)
        {
            var photo = await MediaPicker.CapturePhotoAsync();
            _photoImage.Source = ImageSource.FromFile(photo.FileName);
        }
    }
    private async void ButtonSave_Clicked(object sender, EventArgs e)
    {
        bool receiveFlag;

        CreateTable sendMedicineRecord = new CreateTable()
        {
            MedicineName = MedicineName.Text,
            MedicineUsedFor = MedicineUsedFor.Text,
            Quantity = Convert.ToInt64(Quantity.Text),
            ExpiryDate = ExpiryDate.Date.ToString("dd-MM-yyyy")

        };

        receiveFlag = insertRecord.InserMedicinetRecord(sendMedicineRecord);

        if (receiveFlag)
        {
            
          //  await DisplayAlert("Confirmation Page ", sendMedicineRecord.MedicineName + " saved successfully...", "ok");
        }
        else
        {
            //await DisplayAlert("Error Page ", sendMedicineRecord.MedicineName + " not saved", "ok");
        }

        sendMedicineRecord = null;
        //  await Navigation.PopPopupAsync();
        Close(true);
    }
    private void PopupPage_BackgroundClicked(object sender, EventArgs e)
    {
        //  DisplayAlert("Warning!", "Save it or Cancel", "ok");
        var toast = Toast.Make("Save it or Cancel", CommunityToolkit.Maui.Core.ToastDuration.Long, 300);
        toast.Show();
    }
    private void ButtonCancel_Clicked(object sender, EventArgs e)
    {
        //await Navigation.PopPopupAsync();
        Close(false);

    }
}