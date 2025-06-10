using System;

namespace Projekt_Praktyki_Zawodowe
{
    class Program
    {
        static void Main(string[] args)
        {
            var studentManager = new Managers.StudentManager();
            var companyManager = new Managers.CompanyManager();
            var registrationManager = new Managers.RegistrationManager();
            var agreementManager = new Managers.AgreementManager();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== MENU ===");
                Console.WriteLine("1. Dodaj studenta");
                Console.WriteLine("2. Dodaj firmę");
                Console.WriteLine("3. Wyświetl studentów");
                Console.WriteLine("4. Wyświetl firmy");
                Console.WriteLine("5. Zarejestruj ucznia do firmy");
                Console.WriteLine("6. Pokaż uczniów przypisanych do firmy");
                Console.WriteLine("7. Wygeneruj umowę praktyk");
                Console.WriteLine("--- Opcje tymczasowe ---");
                Console.WriteLine("8. Usuń studenta");
                Console.WriteLine("9. Usuń firmę");
                Console.WriteLine("------------------------");
                Console.WriteLine("0. Wyjście");
                Console.Write("Wybierz opcję: ");
                var key = Console.ReadLine();

                switch (key)
                {
                    case "1": studentManager.AddStudent(); break;
                    case "2": companyManager.AddCompany(); break;
                    case "3": studentManager.ListStudents(); break;
                    case "4": companyManager.ListCompanies(); break;
                    case "5": registrationManager.RegisterStudentToCompany(); break;
                    case "6": registrationManager.ShowStudentsInCompany(); break;
                    case "7": agreementManager.GenerateAgreement(); break;
                    case "8": studentManager.DeleteStudent(); break;
                    case "9": companyManager.DeleteCompany(); break;
                    case "0": return;
                    default: Console.WriteLine("Nieprawidłowa opcja."); break;
                }

                Console.WriteLine("\nNaciśnij dowolny klawisz...");
                Console.ReadKey();
            }
        }
    }
}
