using AutoMapper;
using MultiLanguage.Multi_Lingual;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.SettingManagement;

namespace MultiLanguage.Mapping
{
    public static class AutoMapExtensions
    {
        public static CreateMultiLingualMapResult<TMultiLingualEntity, TTranslation, TDestination> CreateMultiLingualMap<TMultiLingualEntity, TMultiLingualEntityPrimaryKey, TTranslation, TTranslationPrimaryKey, TDestination>(
        this IMapperConfigurationExpression configuration,
        MultiLingualMapContext multiLingualMapContext,
        bool fallbackToParentCultures = false)
        where TMultiLingualEntity : IMultiLingualEntity<TTranslation>
        where TTranslation : class, IEntityTranslation<TMultiLingualEntity, TMultiLingualEntityPrimaryKey>, IEntity<TTranslationPrimaryKey>
        {
            return new CreateMultiLingualMapResult<TMultiLingualEntity, TTranslation, TDestination>
            {
                TranslationMap = configuration.CreateMap<TTranslation, TDestination>(),
                EntityMap = configuration.CreateMap<TMultiLingualEntity, TDestination>().BeforeMap(delegate (TMultiLingualEntity source, TDestination destination, ResolutionContext context)
                {
                    if (!source.Translations.IsNullOrEmpty())
                    {
                        TTranslation val = source.Translations.FirstOrDefault((pt) => pt.Language == CultureInfo.CurrentUICulture.Name);
                        if (val != null)
                        {
                            context.Mapper.Map(val, destination);
                        }
                        else
                        {
                            if (fallbackToParentCultures)
                            {
                                val = GeTranslationBasedOnCulturalRecursive<TMultiLingualEntity, TMultiLingualEntityPrimaryKey, TTranslation>(CultureInfo.CurrentUICulture.Parent, source.Translations, 0);
                                if (val != null)
                                {
                                    context.Mapper.Map(val, destination);
                                    return;
                                }
                            }

                            string defaultLanguage = multiLingualMapContext.SettingManager.GetOrNullDefaultAsync("Abp.Localization.DefaultLanguageName").GetAwaiter().GetResult();
                            val = source.Translations.FirstOrDefault((pt) => pt.Language == defaultLanguage);
                            if (val != null)
                            {
                                context.Mapper.Map(val, destination);
                            }
                            else
                            {
                                val = source.Translations.FirstOrDefault();
                                if (val != null)
                                {
                                    context.Mapper.Map(val, destination);
                                }
                            }
                        }
                    }
                })
            };
        }

        private static TTranslation GeTranslationBasedOnCulturalRecursive<TMultiLingualEntity, TMultiLingualEntityPrimaryKey, TTranslation>(
            CultureInfo culture,
            ICollection<TTranslation> translations,
            int currentDepth)
            where TTranslation : class, IEntityTranslation<TMultiLingualEntity, TMultiLingualEntityPrimaryKey>
        {
            if (culture == null || culture.Name.IsNullOrWhiteSpace() || translations.IsNullOrEmpty() || currentDepth > 5)
            {
                return null;
            }

            return translations.FirstOrDefault((pt) => pt.Language.Equals(culture.Name, StringComparison.OrdinalIgnoreCase))
                   ?? GeTranslationBasedOnCulturalRecursive<TMultiLingualEntity, TMultiLingualEntityPrimaryKey, TTranslation>(culture.Parent, translations, currentDepth + 1);
        }

        public static CreateMultiLingualMapResult<TMultiLingualEntity, TTranslation, TDestination> CreateMultiLingualMap<TMultiLingualEntity, TTranslation, TDestination>(
        this IMapperConfigurationExpression configuration,
        MultiLingualMapContext multiLingualMapContext,
        bool fallbackToParentCultures = false)
        where TMultiLingualEntity : IMultiLingualEntity<TTranslation>
        where TTranslation : class, IEntityTranslation<TMultiLingualEntity, int>, IEntity<int>
        {
            return configuration.CreateMultiLingualMap<TMultiLingualEntity, int, TTranslation, int, TDestination>(multiLingualMapContext, fallbackToParentCultures);
        }

    }

}
