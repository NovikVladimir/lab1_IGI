using System;
using System.Collections.Generic;
using System.Text;

namespace lab1.Models
{
    public class Disease
    {
        public int DiseaseID { get; set; }
        public string DiseaseName { get; set; }
        public string DiseaseSymptoms { get; set; }
        public string DiseaseDuration { get; set; }
        public string DiseaseConsequences { get; set; }
        public int? medicineID { get; set; }
        public virtual Medicine Medicine { get; set; }
        public virtual ICollection<Patient> Patients { get; set; }
    }
}
