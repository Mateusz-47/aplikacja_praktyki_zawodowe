using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Praktyki_Zawodowe.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Supervisor { get; set; }
        public string? Address { get; set; }
        public int MaxPlaces { get; set; }
    }
}
