using System;
using System.Collections.Generic;
using System.Text;

namespace lab1.Models
{
    public class Patient // DischargeDate, Illness, Department, AttendingPhysician, ResultOfTreatment
    {
        public int PatientID { get; set; }
        public string PatientName { get; set; }
        public int PatientAge { get; set; }
        public string PatientGender { get; set; }
        public string PatientAdress { get; set; }
        public string PatientTelephone { get; set; }
        public string PatientDateOfHospitalization { get; set; }
        public string PatientDischargeDate { get; set; }
        public string PatientDisease { get; set; }
        public string PatientDepartment { get; set; }
        public string PatientAttendingPhysician { get; set; }
        public string PatientResultOfTreatment { get; set; }
        public int? diseaseID { get; set; }
        public virtual Disease Disease { get; set; }
    }
}
