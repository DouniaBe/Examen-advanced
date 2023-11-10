using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_DatabaseConnection;

namespace WPF_DatabaseConnection.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        // Nieuw veld voor de beschrijving van het product
        [StringLength(500)] // Pas de maximale lengte aan op basis van je behoeften
        public string Description { get; set; }
    }
}
