using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gruppo3Esame.Models
{
    public class Employee
    {
        public int Id { get; set; }

        public string Nome { get; set; }
        public string Cognome { get; set; }
        public DateTime BirthDay { get; set; }
        public string NIN { get; set; }
        public double DailyCost { get; set; }

       
    }
}
