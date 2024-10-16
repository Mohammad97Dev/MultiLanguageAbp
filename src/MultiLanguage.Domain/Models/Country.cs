using MultiLanguage.Multi_Lingual;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace MultiLanguage.Models
{
    public class Country : FullAuditedEntity<int>, IMultiLingualEntity<CountryTranslation>
    {

        [JsonIgnore]
        public virtual ICollection<CountryTranslation> Translations { get; set; }
    }
}
