using System;
using System.Collections.Generic;
using System.Text;

namespace lab1.Models
{
    public class Medicine
    {
        public int MedicineID { get; set; }
        public string MedicineName { get; set; }
        public string MedicineIndications { get; set; }
        public string MedicineContraindications { get; set; }
        public string MedicineManufacturer { get; set; }
        public string MedicinePackaging { get; set; }
        public string MedicineDosage { get; set; }
        public virtual ICollection<Treatment> Treatments { get; set; }
    }
}
