
using SQLite;


namespace MedicineProject.Model
{
    class UpdateRecord
    {
        string dbPath = DataBaseConnection._dbPath;
        public bool updateMedicineRecord(CreateTable getMedicineDetail)
        {
            bool sendFlag = false;
            var dbConn = new SQLiteConnection(dbPath);


            CreateTable updateMedicineRecord = new CreateTable()
            {
                Id = getMedicineDetail.Id,
                MedicineName = getMedicineDetail.MedicineName,
                MedicineUsedFor = getMedicineDetail.MedicineUsedFor,
                Quantity = getMedicineDetail.Quantity,
                ExpiryDate = getMedicineDetail.ExpiryDate
            };

            try
            {
                dbConn.Update(updateMedicineRecord);
                sendFlag = true;
            }
            catch (Exception)
            {
                sendFlag = false;
            }

            return sendFlag;

        }
    }
}
