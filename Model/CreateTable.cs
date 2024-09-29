using SQLite;


namespace MedicineProject.Model
{
    public class CreateTable
    {
        [PrimaryKey]
        public Int64 Id { get; set; }
        public string UserID { get; set; }
        public string MedicineName { get; set; }
        public string MedicineUsedFor { get; set; }
        public Int64 Quantity { get; set; }
        public String ExpiryDate { get; set; }
        public string MedicineImage { get; set; }
        public bool Status { get; set; }
        public string RecordAddDate { get; set; }

        public override string ToString()
        {
            return this.MedicineName + "(" + this.MedicineUsedFor + ")" + "(" + this.Quantity + ")" + 
                    "(" + this.ExpiryDate + ")" + "(" + this.MedicineImage + ")" + "(" + this.Status+ ")" + "(" + this.RecordAddDate + ")";
        }
    }

   

}