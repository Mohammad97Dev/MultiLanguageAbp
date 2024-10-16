using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiLanguage.Multi_Lingual
{
    public interface IEntityTranslation
    {
        string Language { get; set; }
    }

    public interface IEntityTranslation<TEntity> : IEntityTranslation<TEntity, int>, IEntityTranslation
    {
    }

    public interface IEntityTranslation<TEntity, TPrimaryKeyOfMultiLingualEntity> : IEntityTranslation
    {
        TEntity Core { get; set; }

        TPrimaryKeyOfMultiLingualEntity CoreId { get; set; }
    }
}
