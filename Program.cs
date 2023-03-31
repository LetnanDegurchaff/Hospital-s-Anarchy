using System;
using System.Collections.Generic;
using System.Linq;

namespace HospitalsAnarchy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Hospital hospital = new Hospital();
            hospital.Work();
        }
    }

    class Hospital
    {
        private List<Patient> _patients;

        public Hospital()
        {
            const int patientsCount = 5;
            _patients = new List<Patient>();
            PatientCreator patientsCreator = new PatientCreator();

            for (int i = 0; i < patientsCount; i++)
            {
                _patients.Add(patientsCreator.CreatePatient());
            }
        }

        public void Work()
        {
            //ShowPatients();
            //_patients = SortPatientsByFullNames(_patients);
            //ConsoleOutputMethods.WriteRedText("Отсортированные по ФИО:\n");
            //ShowPatients();
            const string CommandShowPatients = "1";
            const string SortByFullNames = "2";
            const string SortByFullAge = "3";
            const string ShowPatientsWithOneDisease = "4";
            const string CommandExit = "5";

            bool IsExit = false;

            do
            {
                Console.WriteLine("Больница:\n" +
                $"{CommandShowPatients} - Показать список больных\n" +
                $"{SortByFullNames} - Отсортировать список по ФИО\n" +
                $"{SortByFullAge} - Отсортировать список по возрасту\n" +
                $"{ShowPatientsWithOneDisease} - Показать больных с конкретным заболеванием\n" +
                $"{CommandExit} - Выйти из программы\n");

                string input = Console.ReadLine();
                Console.Clear();

                switch (input)
                {
                    case CommandShowPatients:
                        ShowPatients(_patients);
                        break;
                    case SortByFullNames:
                        _patients = SortPatientsByFullNames(_patients);
                        break;
                    case SortByFullAge:
                        _patients = SortPatientsByAge(_patients);
                        break;
                    case ShowPatientsWithOneDisease:
                        ShowPatientsByDiseases(_patients);
                        break;
                    case CommandExit:
                        IsExit = true;
                        break;
                }
            }
            while (IsExit == false);
        }

        private List<Patient> SortPatientsByFullNames(List<Patient> patients)
        {
            var sortedPatients = patients.OrderBy(patient => patient.FullName).ToList();

            return sortedPatients;
        }

        private List<Patient> SortPatientsByAge(List<Patient> patients)
        {
            var sortedPatients = patients.OrderBy(patient => patient.Age).ToList();

            return sortedPatients;
        }

        private void ShowPatientsByDiseases(List<Patient> patients)
        {
            Console.WriteLine("Введите болезнь:\n");
            string diseas = Console.ReadLine();

            var sortedPatients = patients.Where
                (patient => patient.Disease.ToLower() == diseas.ToLower()).ToList();

            ShowPatients(sortedPatients);
        }

        private void ShowPatients(List<Patient> patients)
        {
            if (patients.Count > 0)
            {
                foreach (var patient in patients)
                {
                    Console.WriteLine($"{patient.FullName}\n" +
                        $"Возраст - {patient.Age}\n" +
                        $"Заболевание - {patient.Disease}\n");
                }
            }
            else
            {
                Console.WriteLine("Нет таких пациентов");
            }

            Console.ReadLine();
            Console.Clear();
        }
    }

    class Patient
    {
        public Patient(string fullname, int age, string diseas)
        {
            FullName = fullname;
            Age = age;
            Disease = diseas;
        }

        public string FullName { get; private set; }
        public int Age { get; private set; }
        public string Disease { get; private set; }
    }

    class PatientCreator
    {
        const int MaximumAge = 14;
        const int MinimumAge = 0;

        private Random _random = new Random();
        private List<string> _fullNames;
        private List<string> _diseases;

        public PatientCreator()
        {
            _fullNames = new List<string>();
            _diseases = new List<string>();

            _fullNames.Add("Пупкин Василий Викторович");
            _fullNames.Add("Сафронов Алексей Николаевич");
            _fullNames.Add("Табуретов Биба Васильевич");
            _fullNames.Add("Табуретов Боба Васильевич");
            _fullNames.Add("Котофеев Нурлан Барсикович");
            _fullNames.Add("Рыбаков Александр Юрьевич");
            _fullNames.Add("Владимиров Владимир Владимирович");

            _diseases.Add("Грипп");
            _diseases.Add("Перелом");
            _diseases.Add("Рак");
        }

        public Patient CreatePatient()
        {
            string fullName = _fullNames[_random.Next(0, _fullNames.Count)];
            int age = _random.Next(MinimumAge, MaximumAge);
            string diseas = _diseases[_random.Next(0, _diseases.Count)];
            //
            return new Patient(fullName, age, diseas);
        }
    }

    static class ConsoleOutputMethods
    {
        public static void WriteRedText(string text)
        {
            ConsoleColor tempColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Console.ForegroundColor = tempColor;
        }
    }
}