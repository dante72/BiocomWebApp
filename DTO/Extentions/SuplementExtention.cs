using BiocomWebApp.Database;
using BiocomWebApp.Database.Entity;
using Microsoft.EntityFrameworkCore;

namespace BiocomWebApp.DTO.Extentions
{
    public static class SuplementExtention
    {
        public static Database.Entity.Supplement[] Map(this DTO.Supplement[] supplementsDto, BiocomContext context)
        {
            var supplements = new List<Database.Entity.Supplement>();
            foreach (var supplementDto in supplementsDto)
            {
                var supplement = context.Supplements.FirstOrDefault(x => x.Name == supplementDto.Name);

                if (supplement == null)
                {
                    supplement = new Database.Entity.Supplement();
                    supplement.Name = supplementDto.Name;
                    
                    foreach (var substanse in supplementDto.Substances)
                    {
                        var sumplementPart = new Database.Entity.SupplementPart()
                        {
                            SubstanceValue = substanse.CurrentValue,
                            Diet = supplement
                        };

                        var dbSubstanse = context.Substances.FirstOrDefault(x => x.Name == substanse.Name);
                        if (dbSubstanse != null)
                        {
                            sumplementPart.SubstanceId = dbSubstanse.Id;
                        }
                        else
                        {
                            sumplementPart.Substance = substanse.Map();
                        }

                        supplement.DietParts.Add(sumplementPart);
                        context.AddRange(sumplementPart);
                    }
                }

                supplements.Add(supplement);
            }

            return supplements.ToArray();
        }

        public static DTO.Supplement[] Map(this Database.Entity.Supplement[] supplements, BiocomContext context)
        {
            var supplementDtos = new List<DTO.Supplement>();
            foreach (var supplement in supplements)
            {
                var supplementDto = new DTO.Supplement()
                {
                    Energy = supplement.Energy,
                    Name = supplement.Name
                };

                var substances = new List<Substance>();

                if (supplement.DietParts == null || supplement.DietParts.Count == 0)
                {
                    supplement.DietParts = context.DietParts
                        .Include(d => d.Substance)
                        .Where(p => p.DietId == supplement.Id)
                        .ToArray();
                }

                foreach (var part in supplement.DietParts)
                {
                    var substance = new Substance()
                    {
                        CurrentValue = part.SubstanceValue,
                        Name = part.Substance.Name,
                        MinNormValue = part.Substance.MinNormValue,
                        MaxNormValue = part.Substance.MaxNormValue
                    };

                    substances.Add(substance);
                }

                supplementDto.Substances = substances.ToArray();
                supplementDtos.Add(supplementDto);
            }

            return supplementDtos.ToArray();
        }
    }
}
