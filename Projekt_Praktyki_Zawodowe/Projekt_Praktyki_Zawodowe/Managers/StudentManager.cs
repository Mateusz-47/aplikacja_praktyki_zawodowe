using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using Dapper;
using Projekt_Praktyki_Zawodowe.Helpers;
using Projekt_Praktyki_Zawodowe.Models;

namespace Projekt_Praktyki_Zawodowe.Managers
{
    public class StudentManager
    {
        public void AddStudent()
        {
            Console.Write("Imię: "); var first = Console.ReadLine();
            Console.Write("Nazwisko: "); var last = Console.ReadLine();
            Console.Write("Klasa: "); var cls = Console.ReadLine();
            Console.Write("Telefon: "); var phone = Console.ReadLine();
            Console.Write("Email: "); var email = Console.ReadLine();

            using var db = DbHelper.GetConnection();
            const string sql = @"INSERT INTO Students (FirstName, LastName, Class, Phone, Email)
                                 VALUES (@FirstName, @LastName, @Class, @Phone, @Email)";
            db.Execute(sql, new { FirstName = first, LastName = last, Class = cls, Phone = phone, Email = email });

            Console.WriteLine("\nDodano studenta.");
        }

        public void ListStudents()
        {
            using var db = DbHelper.GetConnection();
            var students = db.Query<Student>("SELECT * FROM Students");
            Console.WriteLine("--- Studenci ---");
            foreach (var s in students)
                Console.WriteLine($"{s.Id}. {s.FirstName} {s.LastName}, {s.Class}, {s.Phone}, {s.Email}");
            Console.WriteLine();
        }

        public void DeleteStudent()
        {
            ListStudents();
            Console.WriteLine();
            Console.Write("ID studenta do usunięcia: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("\nBłędne ID.");
                return;
            }

            using var db = DbHelper.GetConnection();
            db.Execute("DELETE FROM Students WHERE Id = @Id", new { Id = id });

            Console.WriteLine("\nUsunięto studenta.");
        }
    }
}
