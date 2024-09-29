
using SQLite;



namespace MedicineProject.Model
{
   public class InsertRecord
    {
        string _dbPath = DataBaseConnection._dbPath;

        //This method is used to insert record in dabase table
        public bool InserMedicinetRecord(CreateTable getMedicineRecord)
        {
            bool flag =false;
            var dbConn = new SQLiteConnection(_dbPath);
            var maxPrimaryKey = dbConn.Table<CreateTable>().OrderByDescending(mr => mr.Id).FirstOrDefault();

            CreateTable insertMedicineRecord = new CreateTable()
            {
                Id = (maxPrimaryKey == null ? 1 : maxPrimaryKey.Id + 1),

                MedicineName = getMedicineRecord.MedicineName,
                MedicineUsedFor = getMedicineRecord.MedicineUsedFor,
                Quantity = getMedicineRecord.Quantity,
                ExpiryDate = getMedicineRecord.ExpiryDate,
                Status = true,
                RecordAddDate = GetTodayDate()
            };

            try
            {
                dbConn.Insert(insertMedicineRecord);
                flag = true;
            }
            catch (Exception)
            {
                flag = false;
            }
            return flag;
        }


        //This is used to get today date 
        public string GetTodayDate()
        {
            string today_Date = DateTime.Now.ToString("dd-MM-yyyy");
            return today_Date;
        }

    }
}
