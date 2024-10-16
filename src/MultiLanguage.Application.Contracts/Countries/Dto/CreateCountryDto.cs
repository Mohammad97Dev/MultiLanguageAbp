using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiLanguage.Countries.Dto
{
    public class CreateCountryDto : IValidatableObject
    {
        public List<CountryTranslationDto> Translations { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Translations is null || Translations.Count < 2)
            {
                yield return new ValidationResult("Translations must contain at least two elements", new[] { nameof(Translations) });
            }
        }
    }
}
