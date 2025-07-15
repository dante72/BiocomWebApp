using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiocomWebApp.Database.Entity
{
    public class Diet
    {
        [Key]
        public int Id { get; set; }
        public double Energy { get; set; }
        public Diagnostic Diagnostic { get; set; } = null!;

        public ICollection<DietPart> DietParts = new List<DietPart>();
    }
}
