using System;
using System.Collections.Generic;
using System.Text;

namespace lab1.Models
{
   public class Treatment
    {
        public int TreatmentID { get; set; }
        public string TreatmentDisease { get; set; }
        public string TreatmentMedication { get; set; }
        public string TreatmentDate { get; set; }
        public string TreatmentDosage { get; set; }
        public string TreatmentDurationOfTreatment { get; set; }
        public int? diseaseID { get; set; }
        public int? medicineID { get; set; }
        public virtual Disease Disease { get; set; }
        public virtual Medicine Medicine { get; set; }
    }
}
