using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_DatabaseConnection.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [StringLength(100)] // Het is goed gebruik om wachtwoorden te limiteren in lengte
        public string Password { get; set; }

        // Voeg andere relevante eigenschappen toe
    }
}
