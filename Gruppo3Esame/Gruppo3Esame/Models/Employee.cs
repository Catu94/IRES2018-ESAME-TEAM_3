using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gruppo3Esame.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Employee must have a name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Employee must have a lastName")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Employee must have a birthday")]
        public DateTime BirthDay { get; set; }

        [Required(ErrorMessage = "Employee must have a NIN")]
        [MinLength(16)]
        [MaxLength(16)]
        public string NIN { get; set; }

        [Required(ErrorMessage = "Employee must have a daily salary")]
        public double DailyCost { get; set; }

        public ICollection<ProjectEmployee> PE { get; set; }
    }
}
