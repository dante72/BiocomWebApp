using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace BiocomWebApp.Database.Entity
{
    public class DietPart
    {
        [Key]
        public int Id { set; get; }
        public double SubstanceValue { get; set; }
        public int SubstanceId { get; set; }
        public Substance Substance { get; set; } = null!;
        public int DietId { get; set; }
        public Diet Diet { get; set; }
    }
}
