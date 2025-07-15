using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace BiocomWebApp.Database.Entity
{
    public class Substance
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double MinNormValue {  get; set; }
        public double? MaxNormValue { get; set; }
        public List<SubstanceType> Types { get; } = new List<SubstanceType>();

        public Substance(string name, double min, double? max, params SubstanceType[] types)
        {
            Name = name;
            MinNormValue = min;
            MaxNormValue = max;
            Types.AddRange(types);
        }

        public Substance() { }
    }
}
