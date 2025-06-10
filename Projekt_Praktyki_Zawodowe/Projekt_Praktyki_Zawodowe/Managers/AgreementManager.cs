using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO;
using Dapper;
using Projekt_Praktyki_Zawodowe.Helpers;
using Projekt_Praktyki_Zawodowe.Managers;

namespace Projekt_Praktyki_Zawodowe.Managers
{
    public class AgreementManager
    {
        public void GenerateAgreement()
        {
            Console.WriteLine("--- Generowanie umowy ---");
            new StudentManager().ListStudents();
            Console.WriteLine();
            Console.Write("ID ucznia: ");
            if (!int.TryParse(Console.ReadLine(), out int studentId)) { Console.WriteLine("\nBłędne ID."); return; }

            using var db = DbHelper.GetConnection();
            var sql = @"SELECT s.FirstName, s.LastName, s.Class, s.Email, c.Name AS CompanyName, c.Supervisor, c.Address
                FROM Students s
                JOIN Registrations r ON s.Id = r.StudentId
                JOIN Companies c ON r.CompanyId = c.Id
                WHERE s.Id = @StudentId";

            var result = db.QueryFirstOrDefault(sql, new { StudentId = studentId });
            if (result == null)
            {
                Console.WriteLine("\nNie znaleziono przypisanej firmy dla ucznia.");
                return;
            }

            string content = $"UMOWA PRAKTYK ZAWODOWYCH\n\n" +
                             $"Uczeń: {result.FirstName} {result.LastName}\n" +
                             $"Klasa: {result.Class}\n\n" +
                             $"Firma: {result.CompanyName}\n" +
                             $"Opiekun: {result.Supervisor}\n" +
                             $"Adres: {result.Address}\n\n" +
                             $"Data zawarcia umowy: {DateTime.Now:yyyy-MM-dd}\n\n" +
                             $"Podpisy:\nUczeń _______________\nPrzedstawiciel firmy _______________";

            string directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Umowy");
            Directory.CreateDirectory(directory);
            string fileName = Path.Combine(directory, $"Umowa_{result.FirstName}_{result.LastName}.txt");

            try
            {
                File.WriteAllText(fileName, content); // <--- TO DODAJ
                Console.WriteLine($"\n✅ Wygenerowano umowę: {fileName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Błąd podczas zapisu pliku: {ex.Message}");
                return;
            }

            // Automatyczne wysyłanie e-maila do studenta
            if (!string.IsNullOrWhiteSpace(result.Email))
            {
                EmailSender.SendAgreement(result.Email, fileName);
            }
            else
            {
                Console.WriteLine("Brak adresu e-mail studenta. Umowa nie została wysłana.");
            }
        }

    }
}
