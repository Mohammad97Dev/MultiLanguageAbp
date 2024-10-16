using MultiLanguage.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace MultiLanguage.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(MultiLanguageEntityFrameworkCoreModule),
    typeof(MultiLanguageApplicationContractsModule)
)]
public class MultiLanguageDbMigratorModule : AbpModule
{
}
