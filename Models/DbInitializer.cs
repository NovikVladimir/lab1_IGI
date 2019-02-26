using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace lab1.Models
{
    public static class DbInitializer
    {
        public static void Initialize(Context db)
        {
            db.Database.EnsureCreated();

            if (db.Patients.Any())
            {
                return;  
            }

            Random rand = new Random();
            int id;
            string[] medicines = { "Глицин", "Валидол", "Валерьянка", "Аскорбинка", "Мезим" };
            string[] medicinesIndications = { "детям до 18", "детям после 18", "всем", "беременные", "студенты" };
            string[] medicinesContraindications = { "пенсионеры", "беременные", "спокойные", "не беременные", "отсутствует" };
            string[] medicinesManufacturers = { "ОАО \"Зорька\"", "ООО \"Красный желтый\"", "ОАО \"ЛИИТ\"", "ОАО \"ЗОЖ\"", "ОАО \"ГГТУ\"" };
            for (int i = 0; i < 5; i++)
            {
                db.Medicines.Add(new Medicine
                {
                    MedicineName = medicines[rand.Next(5)],
                    MedicineIndications = medicinesIndications[rand.Next(5)],
                    MedicineContraindications = medicinesContraindications[rand.Next(5)],
                    MedicineManufacturer = medicinesManufacturers[rand.Next(5)],
                    MedicinePackaging = (rand.Next(10) + 1).ToString() + "шт.",
                    MedicineDosage = (rand.Next(10) + 1).ToString() + "шт. каждые " + (rand.Next(10) + 1).ToString() + "ч."
                });
            }
            db.SaveChanges();

            string[] diseases = { "Анемия", "ВИЧ", "Пневномия", "Грипп", "Ангина" };
            string[] symptoms = { "слабость", "никаких", "кашель", "насморк", "ослабление", "боль", "голос", "сыпь", "чувствительность", "температура" };
            for (int i = 0; i < 5; i++)
            {
                id = rand.Next(1, 6);
                db.Diseases.Add(new Disease
                {
                    DiseaseName = diseases[id - 1],
                    DiseaseSymptoms = symptoms[id - 1] + " " + symptoms[id + 4],
                    DiseaseDuration = (rand.Next(50) + 1).ToString() + " дн.",
                    DiseaseConsequences = symptoms[rand.Next(5)],
                    medicineID = id
            });
            }
            db.SaveChanges();

            string[] patientNames = { "Владимир", "Станислав", "Дмитрий", "Адам", "Никита" };
            string[] patientGenders = { "м", "ж" };
            string[] patientAdresses = { "пр. Октября", "пр. Речицкий", "ул. Барыкина", "пр. Ленина", "ул. Советская" };
            string[] patientAttendingPhysicians = { "П.О. Сухой", "К.С. Курочка", "Г.П. Косинов", "А.А. Саприко", "Н.В. Самовендюк" };
            string[] patientResultsOfTreatment = { "положительный", "отрицательный" };
            for (int i = 0; i < 5; i++)
            {
                id = rand.Next(1, 6);
                db.Patients.Add(new Patient
                {
                    PatientName = patientNames[rand.Next(5)],
                    PatientAge = rand.Next(10) + 10,
                    PatientGender = patientGenders[rand.Next(2)],
                    PatientAdress = patientAdresses[rand.Next(5)],
                    PatientTelephone = "+37533" + (rand.Next(8999999) + 100000).ToString(),
                    PatientDateOfHospitalization = "13.02.2019",
                    PatientDischargeDate = "18.03.2019",
                    PatientDisease = diseases[id - 1],
                    PatientDepartment = (rand.Next(5) + 1).ToString(),
                    PatientAttendingPhysician = patientAttendingPhysicians[rand.Next(5)],
                    PatientResultOfTreatment = patientResultsOfTreatment[rand.Next(2)],
                    diseaseID = id
                });
            }
            db.SaveChanges();

            for (int i = 0; i < 5; i++)
            {
                id = rand.Next(1, 6);
                db.Treatmens.Add(new Treatment
                {
                    TreatmentDisease = diseases[id - 1],
                    TreatmentMedication = medicines[id - 1],
                    TreatmentDate = "13.02.2019",
                    TreatmentDosage = (rand.Next(20) + 1).ToString() + "шт.",
                    TreatmentDurationOfTreatment = (rand.Next(20) + 1).ToString() + " дн.",
                    diseaseID = id,
                    medicineID = id
                });
            }
            db.SaveChanges();
        }
    }
}
