using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using lab1.Models;
using System.Collections;

namespace lab1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (Context db = new Context())
            {
                DbInitializer.Initialize(db);
                //Console.WriteLine("====== Будет выполнена выборка данных (нажмите любую клавишу) ========");
                //Console.ReadKey();
                //SelectAllTreatmentRegimens(db);
                //Console.WriteLine("====== Будет выполнена выборка данных (нажмите любую клавишу) ========");
                //Console.ReadKey();
                //SelectTreatmentRegimen(db);
                //Console.WriteLine("====== Будет выполнена выборка данных (нажмите любую клавишу) ========");
                //Console.ReadKey();
                //SelectAllPatients(db);
                //Console.WriteLine("====== Будет выполнена выборка данных (нажмите любую клавишу) ========");
                //Console.ReadKey();
                //SelectPatient(db);
                Console.WriteLine("====== Будет выполнена выборка данных (нажмите любую клавишу) ========");
                Console.ReadKey();
                SelectDiseases(db);
                Console.ReadKey();
            }
        }

        static void Print(string sqltext, IEnumerable items)
        {
            Console.WriteLine(sqltext);
            Console.WriteLine("Записи: ");
            foreach (var item in items)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine();
        }

        static void Insert(Context db)
        {
            Medicine medicine = new Medicine
            {
                MedicineName = "Аспирин",
                MedicineIndications = "детям после 18",
                MedicineContraindications = "отсутствие головных болей",
                MedicineManufacturer = "ООО \"Красный желтый\"",
                MedicinePackaging = "10 таблеток",
                MedicineDosage = "2 таблетки каждые 5 часов"
            };
            Disease disease = new Disease
            {
                DiseaseName = "Аденомиоз",
                DiseaseSymptoms = "боль озноб",
                DiseaseDuration = "40 дн",
                DiseaseConsequences = "утомляемость"
            };

            db.Medicines.Add(medicine);
            db.Diseases.Add(disease);
            db.SaveChanges();

            Treatment treatmen = new Treatment
            {
                TreatmentDisease = disease.DiseaseName,
                TreatmentMedication = medicine.MedicineName,
                TreatmentDate = "13.02.2019",
                TreatmentDosage = "24 таблетки",
                TreatmentDurationOfTreatment = "7 дн",
                diseaseID = disease.DiseaseID,
                medicineID = medicine.MedicineID
            };

            db.Treatmens.Add(treatmen);
            db.SaveChanges();

        }

        static void SelectAllPatients(Context db)
        {
            var queryLINQ = from f in db.Patients
                             join t in db.Diseases
                             on f.diseaseID equals t.DiseaseID
                             orderby f.diseaseID
                             select new
                             {
                                 Имя = f.PatientName,
                                 Возраст = f.PatientAge,
                                 Адрес = f.PatientAdress,
                                 Телефон = f.PatientTelephone,
                                 Дата_госпитализации = f.PatientDateOfHospitalization,
                                 Дата_выписки = f.PatientDischargeDate,
                                 Болезнь = t.DiseaseName,
                                 Лечащий_врач = f.PatientAttendingPhysician
                             };

            string comment = "Результат выполнения запроса на \"Все пациенты\" : \r\n";
            Print(comment, queryLINQ.ToList());
        }

        static void SelectPatient(Context db)
        {
            var patients = from f in db.Patients
                           select new
                           {
                               Имя = f.PatientName,
                               Болезнь = f.PatientDisease,
                               Номер = f.PatientID
                           };
            Print("Список пациентов: ", patients.ToList());
            Console.WriteLine("Введите номер пациента");
            int number = Convert.ToInt32(Console.ReadLine());
            var queryLINQ = from f in db.Patients
                             join t in db.Diseases
                             on f.diseaseID equals t.DiseaseID
                             where f.PatientID == number
                             select new
                             {
                                 Имя = f.PatientName,
                                 Возраст = f.PatientAge,
                                 Адрес = f.PatientAdress,
                                 Телефон = f.PatientTelephone,
                                 Дата_госпитализации = f.PatientDateOfHospitalization,
                                 Дата_выписки = f.PatientDischargeDate,
                                 Болезнь = t.DiseaseName,
                                 Лечащий_врач = f.PatientAttendingPhysician
                             };

            string comment = "Результат выполнения запроса на \"Конкретный пациент\" : \r\n";
            Print(comment, queryLINQ.ToList());
        }

        static void SelectAllTreatmentRegimens(Context db)
        {

            var queryLINQ = from f in db.Diseases
                             join t in db.Medicines
                             on f.medicineID equals t.MedicineID
                             orderby f.DiseaseID
                             select new
                             {
                                 Название_Болезни = f.DiseaseName,
                                 Название_Лекарства = t.MedicineName,
                                 Дозировка = t.MedicineDosage,
                                 В_течении_какого_времени = f.DiseaseDuration
                             };

            string comment = "1. Результат выполнения запроса на \"Все схемы лечения\" : \r\n";
            Print(comment, queryLINQ.ToList());
        }

        static void SelectTreatmentRegimen(Context db)
        {
            var diseases = from f in db.Diseases
                           select new { Название_болезни = f.DiseaseName };
            var diseasesDistinct = diseases.GroupBy(x => x.Название_болезни).Select(y => y.First()).ToList();
            Print("Список болезней: ", diseasesDistinct.ToList());
            Console.WriteLine("Введите название болезни");
            string name = Console.ReadLine();
            var queryLINQ = from f in db.Diseases
                             join t in db.Medicines
                             on f.medicineID equals t.MedicineID
                             where f.DiseaseName == name
                             select new
                             {
                                 Название_Болезни = f.DiseaseName,
                                 Название_Лекарства = t.MedicineName,
                                 Дозировка = t.MedicineDosage,
                                 В_течении_какого_времени = f.DiseaseDuration
                             };

            string comment = "1. Результат выполнения запроса на \"Конкретная схема лечения\" : \r\n";
            Print(comment, queryLINQ);
        }

        static void SelectDiseases(Context db)
        {
            var diseases = from f in db.Diseases
                           select new { Симптомы = f.DiseaseSymptoms };
            var diseasesDistinct = diseases.GroupBy(x => x.Симптомы).Select(y => y.First()).ToList();
            Print("Список симптомов: ", diseasesDistinct.ToList());
            Console.WriteLine("Введите симптомы через пробел");
            string[] symptomsArr = Console.ReadLine().Split(' ');
            //string a;
            //string[] aa;
            //foreach (var i in db.Diseases)
            //{
            //    a = i.DiseaseSymptoms;
            //    aa = a.Split();
            //    bool t = aa.Contains(symptomsArr[0]);
            //    int c = 4;
            //}
            var queryLINQ = from f in db.Diseases
                            where (f.DiseaseSymptoms.Contains(symptomsArr[0]) || (symptomsArr.Length == 2 && f.DiseaseSymptoms.Contains(symptomsArr[0]) && f.DiseaseSymptoms.Contains(symptomsArr[1])))
                            select new
                            {
                                Название_болезни = f.DiseaseName,
                                Симптомы = f.DiseaseSymptoms,
                                Продолжительность = f.DiseaseDuration,
                                Последствия = f.DiseaseConsequences
                            };

            string comment = "Результат выполнения запроса на \"Болезнь по симптомам\" : \r\n";
            Print(comment, queryLINQ.ToList());
        }
    }
}