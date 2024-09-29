using SQLite;



namespace MedicineProject.Model
{
    class DeleteRecord
    {
        string _dbPath = DataBaseConnection._dbPath;

        public bool DeleteMedicinetRecord(CreateTable getMedicineId)
        {
            bool sendFlag = false;
            var dbConn = new SQLiteConnection(_dbPath);
                        
            try
            {
                dbConn.Table<CreateTable>().Delete(x => x.Id == getMedicineId.Id);
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
