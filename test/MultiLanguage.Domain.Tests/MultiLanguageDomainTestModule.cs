using Volo.Abp.Modularity;

namespace MultiLanguage;

[DependsOn(
    typeof(MultiLanguageDomainModule),
    typeof(MultiLanguageTestBaseModule)
)]
public class MultiLanguageDomainTestModule : AbpModule
{

}
