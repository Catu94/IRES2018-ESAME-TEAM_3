using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gruppo3Esame.Models
{
    public class Project
    {
        public int Id { get; set; }

        [MaxLength(5)]
        [MinLength(5)]
        [Required(ErrorMessage = "Key is required")]
        public string Key { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public Category Category { get; set; }

        [Required(ErrorMessage = "StartDate is required")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "EndDate is required")]
        public DateTime EndDate { get; set; }

        public ICollection<ProjectEmployee> PE { get; set; }
    }
}
