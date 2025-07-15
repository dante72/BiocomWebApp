using BiocomWebApp.Database;
using BiocomWebApp.Database.Entity;

namespace BiocomWebApp.DTO.Extentions
{
    public static class SubstanceExtention
    {
        public static Database.Entity.Substance Map(this DTO.Substance substanceDto)
        {
            return new Database.Entity.Substance()
            {
                Name = substanceDto.Name,
                MinNormValue = substanceDto.MinNormValue,
                MaxNormValue = substanceDto.MaxNormValue
            };
        }
    }
}
