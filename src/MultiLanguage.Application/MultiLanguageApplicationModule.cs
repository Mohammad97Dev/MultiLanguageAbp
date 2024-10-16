using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.Account;
using Volo.Abp.Identity;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Modularity;
using Volo.Abp.TenantManagement;
using System.Diagnostics.Metrics;
using MultiLanguage.Models;
using MultiLanguage.Countries.Dto;
using MultiLanguage.Mapping;

namespace MultiLanguage;

[DependsOn(
    typeof(MultiLanguageDomainModule),
    typeof(MultiLanguageApplicationContractsModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpAccountApplicationModule),
    typeof(AbpTenantManagementApplicationModule),
    typeof(AbpSettingManagementApplicationModule)
    )]
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
                //var settingManager = context.Services.GetRequiredService<ISettingManager>();
                CustomDtoMapper.CreateMappings(cfg, new MultiLingualMapContext(SettingManager));
            });
        });
    }
}
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