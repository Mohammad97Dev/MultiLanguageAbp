using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiLanguage.Multi_Lingual
{
    public interface IMultiLingualEntity<TTranslation> where TTranslation : class, IEntityTranslation
    {
        ICollection<TTranslation> Translations { get; set; }
    }
}
