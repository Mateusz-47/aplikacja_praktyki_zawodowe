using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using Dapper;
using Projekt_Praktyki_Zawodowe.Models;
using Projekt_Praktyki_Zawodowe.Helpers;

namespace Projekt_Praktyki_Zawodowe.Managers
{
    public class CompanyManager
    {
        public void AddCompany()
        {
            Console.Write("Nazwa firmy: "); var name = Console.ReadLine();
            Console.Write("Opiekun: "); var sup = Console.ReadLine();
            Console.Write("Adres: "); var addr = Console.ReadLine();
            Console.Write("Maks. miejsc: "); var max = int.Parse(Console.ReadLine() ?? "0");

            using var db = DbHelper.GetConnection();
            const string sql = @"INSERT INTO Companies (Name, Supervisor, Address, MaxPlaces)
                                 VALUES (@Name, @Supervisor, @Address, @MaxPlaces)";
            db.Execute(sql, new { Name = name, Supervisor = sup, Address = addr, MaxPlaces = max });

            Console.WriteLine("\nDodano firmę.");
        }

        public void ListCompanies()
        {
            using var db = DbHelper.GetConnection();
            var comps = db.Query<Company>("SELECT * FROM Companies");
            Console.WriteLine("--- Firmy ---");
            foreach (var c in comps)
                Console.WriteLine($"{c.Id}. {c.Name}, opiekun: {c.Supervisor}, {c.Address}, miejsca: {c.MaxPlaces}");
            Console.WriteLine();
        }

        public void DeleteCompany()
        {
            ListCompanies();
            Console.WriteLine();
            Console.Write("ID firmy do usunięcia: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("\nBłędne ID.");
                return;
            }

            using var db = DbHelper.GetConnection();
            db.Execute("DELETE FROM Companies WHERE Id = @Id", new { Id = id });

            Console.WriteLine("\nUsunięto firmę.");
        }
    }
}
