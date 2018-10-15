using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gruppo3Esame.ViewModel
{
    public class Login
    {
        [Required(ErrorMessage = "User must have a valid email")]
        [MaxLength(25)]
        public string Email { get; set; }

        [Required(ErrorMessage = "User must have a valid password (at least 8 characters)")]
        [MinLength(8)]
        public string Password { get; set; }
    }
}
