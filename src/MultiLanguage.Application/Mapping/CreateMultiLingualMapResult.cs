using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiLanguage.Mapping
{
    public class CreateMultiLingualMapResult<TMultiLingualEntity, TTranslation, TDestination>
    {
        public IMappingExpression<TTranslation, TDestination> TranslationMap { get; set; }

        public IMappingExpression<TMultiLingualEntity, TDestination> EntityMap { get; set; }
    }
}
