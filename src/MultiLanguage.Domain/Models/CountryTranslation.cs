using MultiLanguage.Multi_Lingual;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Volo.Abp.Domain.Entities.Auditing;

namespace MultiLanguage.Models
{
    public class CountryTranslation : FullAuditedEntity<int>, IEntityTranslation<Country>
    {

        public string Name { get; set; }
        public int CoreId { get; set; }
        [ForeignKey(nameof(CoreId))]
        [JsonIgnore]
        public virtual Country Core { get; set; }


        public string Language { get; set; }
    }
}