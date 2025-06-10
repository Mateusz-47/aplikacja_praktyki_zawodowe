using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using Dapper;
using Projekt_Praktyki_Zawodowe.Models;
using Projekt_Praktyki_Zawodowe.Helpers;
using Projekt_Praktyki_Zawodowe.Managers;

namespace Projekt_Praktyki_Zawodowe.Managers
{
    public class RegistrationManager
    {
        public void RegisterStudentToCompany()
        {
            Console.WriteLine("--- Rejestracja ucznia do firmy ---");
            new StudentManager().ListStudents();
            Console.WriteLine();
            Console.Write("ID ucznia: ");
            if (!int.TryParse(Console.ReadLine(), out int studentId)) { Console.WriteLine("\nBłędne ID."); return; }

            using var db = DbHelper.GetConnection();
            long alreadyRegistered = db.ExecuteScalar<long>("SELECT COUNT(*) FROM Registrations WHERE StudentId = @StudentId", new { StudentId = studentId });
            if (alreadyRegistered > 0)
            {
                Console.WriteLine("\nUczeń jest już zarejestrowany w firmie.");
                return;
            }

            new CompanyManager().ListCompanies();
            Console.WriteLine();
            Console.Write("ID firmy: ");
            if (!int.TryParse(Console.ReadLine(), out int companyId)) { Console.WriteLine("\nBłędne ID."); return; }

            long maxPlaces = db.ExecuteScalar<long>("SELECT MaxPlaces FROM Companies WHERE Id = @Id", new { Id = companyId });
            long currentCount = db.ExecuteScalar<long>("SELECT COUNT(*) FROM Registrations WHERE CompanyId = @CompanyId", new { CompanyId = companyId });

            if (currentCount >= maxPlaces)
            {
                Console.WriteLine("\nBrak miejsc w wybranej firmie.");
                return;
            }

            const string sql = @"INSERT INTO Registrations (StudentId, CompanyId, RegisteredAt)
                                 VALUES (@StudentId, @CompanyId, @RegisteredAt)";
            db.Execute(sql, new { StudentId = studentId, CompanyId = companyId, RegisteredAt = DateTime.Now });

            Console.WriteLine("\nZarejestrowano ucznia na praktyki.");
        }

        public void ShowStudentsInCompany()
        {
            Console.WriteLine("--- Uczniowie przypisani do firmy ---");
            new CompanyManager().ListCompanies();
            Console.WriteLine();
            Console.Write("ID firmy: ");
            if (!int.TryParse(Console.ReadLine(), out int companyId)) { Console.WriteLine("\nBłędne ID."); return; }

            using var db = DbHelper.GetConnection();
            const string sql = @"SELECT s.Id, s.FirstName, s.LastName, s.Class, s.Phone, s.Email
                                 FROM Students s
                                 JOIN Registrations r ON s.Id = r.StudentId
                                 WHERE r.CompanyId = @CompanyId";

            var students = db.Query<Student>(sql, new { CompanyId = companyId });
            Console.WriteLine($"\nUczniowie przypisani do firmy ID: {companyId}");
            foreach (var s in students)
            {
                Console.WriteLine($"{s.Id}. {s.FirstName} {s.LastName}, {s.Class}, {s.Phone}, {s.Email}");
            }
            Console.WriteLine();
        }
    }
}
