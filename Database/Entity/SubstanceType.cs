using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BiocomWebApp.Database.Entity
{
    public class SubstanceType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<Substance> Substances { get; } = new List<Substance>();
    }
}
