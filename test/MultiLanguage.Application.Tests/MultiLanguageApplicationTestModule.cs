using Volo.Abp.Modularity;

namespace MultiLanguage;

[DependsOn(
    typeof(MultiLanguageApplicationModule),
    typeof(MultiLanguageDomainTestModule)
)]
public class MultiLanguageApplicationTestModule : AbpModule
{

}
