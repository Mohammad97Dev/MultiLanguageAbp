Hello every body 
in this Project i will explain how to Enable Multi Language in Abp.io FramWork 
At First Go to MultiLanguage.Domain.Shared (Shared Layer)
and Add this two main interfaces
the first one with name IEntityTranslation 

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

and the second one with name IMultiLingualEntity

     namespace MultiLanguage.Multi_Lingual
    {
    public interface IMultiLingualEntity<TTranslation> where TTranslation : class, IEntityTranslation
    {
        ICollection<TTranslation> Translations { get; set; }
    }
    }

after that go to Application Layer MultiLanguage.Application and add this three Classes
first one with name AutoMapExtensions

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
Second one With Name CreateMultiLingualMapResult

    public class CreateMultiLingualMapResult<TMultiLingualEntity, TTranslation, TDestination>
    {
       public IMappingExpression<TTranslation, TDestination> TranslationMap { get; set; }

       public IMappingExpression<TMultiLingualEntity, TDestination> EntityMap { get; set; }
    }

and third one with Name MultiLingualMapContext 
 
    public class MultiLingualMapContext
    {
     public ISettingManager SettingManager { get; set; }

     public MultiLingualMapContext(ISettingManager settingManager)
     {
         SettingManager = settingManager;
     }
    } 
Note* You Can Put it directly in Application Layer or in Folder in Application Layer.

After All that Now we can Create Country Entity and it's own Translations
in Domain Layer we can Define the Country that inherited From IMultiLingualEntity and pass CountryTranslation Entity Like that

     public class Country : FullAuditedEntity<int>, IMultiLingualEntity<CountryTranslation>
    {

      [JsonIgnore]
      public virtual ICollection<CountryTranslation> Translations { get; set; }
    } 
and don't forget to add [JsonIgnore] DataAnnotion to make JsonCycle Stop !
now we need to Create CountryTranslation that Inherited From IEntityTranslation and Pass Country Entity Like that 

    public class CountryTranslation : FullAuditedEntity<int>, IEntityTranslation<Country>
    {

      public string Name { get; set; }
      public int CoreId { get; set; }
      [ForeignKey(nameof(CoreId))]
      [JsonIgnore]
      public virtual Country Core { get; set; }


      public string Language { get; set; }
    }

now in Contrast Layer we Will Created ICountryAppService And Dto Classes and the new one here is CountryTranslationDto which has Name And Language Properties 

     public class CountryTranslationDto
     {
       /// <summary>
       /// Name
       /// </summary>
       public string Name { get; set; }
       /// <summary>
       /// Language
       /// </summary>
       public string Language { get; set; }
     }

now Create CreateCountryDto
 
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
as you see we add a list of CountryTranslationDto and we have add Validation for it. this deepend on your system anyway if you need to enter more than one language 
now Create CountryDto Class 

     public class CountryDto : FullAuditedEntityDto<int>
    {
     public string Name { get; set; }
     public List<CountryTranslationDto> Translations { get; set; }
    }

as you see there are Name and List of CountryTranslationDto , let me explain if you have added in CountryTranslation more that one Property like (Name , Description, Address) you need to add it in Dto and the AutoMapper will return it as language of Application 
now we will go to Application Layer and Add CountryAppService like that

       public class CountryAppService : CrudAppService<Country, CountryDto, int, PagedCountryResultRequestDto, CreateCountryDto, UpdateCountryDto>, ICountryAppService
    {

        public CountryAppService(IRepository<Country, int> repository) : base(repository)
        {
        }
        public override async Task<CountryDto> GetAsync(int id)
        {
            var country = Repository.WithDetailsAsync(x => x.Translations).Result.Where(c => c.Id == id)
                    .FirstOrDefault() ??
                    throw new EntityNotFoundException(typeof(Country), id);
            return ObjectMapper.Map<Country, CountryDto>(country);


        }
        public override Task<PagedResultDto<CountryDto>> GetListAsync(PagedCountryResultRequestDto input)
        {
            return base.GetListAsync(input);
        }
        protected override Task<IQueryable<Country>> CreateFilteredQueryAsync(PagedCountryResultRequestDto input)
        {
            return Repository.WithDetailsAsync(x => x.Translations);

        }
    }
there are some of Dto i have created without Ecplain it here (PagedCountryResultRequestDto, UpdateCountryDto)
and of course when you create New table to save translations you need to include it to retrieve the data !

now we need to Configure the Mapping and add CustomMapper 
in the Module of Application Layer Create static Class with name CustomDtoMapper 

     public static class CustomDtoMapper
    {
    public static void CreateMappings(IAbpAutoMapperConfigurationContext configuration, MultiLingualMapContext context)
    {

        #region Country
        // Country Translation Configuration


        configuration.MapperConfiguration.CreateMultiLingualMap<Country, CountryTranslation, CountryDto>(context).TranslationMap
     .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        #endregion
    }
    }
in this class there is a Method Which Configure the Mapping amd return the name  using AutoMapper 
and if you have another Dto class and you need to add name and Translations for it repeat this code and replca the CountryDto with your new Dto Like this 

          configuration.MapperConfiguration.CreateMultiLingualMap<Country, CountryTranslation, YourNewDto>(context).TranslationMap
     .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        

now we need to Configure CustomMapper in ConfigureServices in Application Module like this 
   
     public class MultiLanguageApplicationModule : AbpModule
     {
    public ISettingManager SettingManager { get; set; }
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<MultiLanguageApplicationModule>();
            options.Configurators.Add(cfg =>
            {
                CustomDtoMapper.CreateMappings(cfg, new MultiLingualMapContext(SettingManager));
            });
        });
    }
    }
the last step it add Mapper For Classes in Application Layer like this under Countries Folder 

        public class CountryMapProfile : Profile
    {
       public CountryMapProfile()
       {
           CreateMap<CreateCountryDto, Country>().ReverseMap();
           CreateMap<UpdateCountryDto, Country>().ReverseMap();
           CreateMap<CountryTranslation, CountryTranslationDto>().ReverseMap();

       }
    }

now we can Run the Application and don't forget to add migration for database.
post first Country Name at two Language (English and Arabic)

![image](https://github.com/user-attachments/assets/954ffa49-5f41-423f-ac84-6f2bf84d82a4)


after added it let me get it from database and my language now is English !

![image](https://github.com/user-attachments/assets/35d40611-5231-4e5a-a242-fbb7d87ea9a7)


now let me Change language to Arabic and call api again 

![image](https://github.com/user-attachments/assets/c806b686-9758-4220-a0ab-06f7d5cdc994)

this api was getById
let us try GetAll ! 


![image](https://github.com/user-attachments/assets/9edbfa63-564a-4c3a-9e08-a9525d0ca2a1)

it's work Successfully!
