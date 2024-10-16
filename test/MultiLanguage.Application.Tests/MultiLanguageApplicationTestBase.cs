using Volo.Abp.Modularity;

namespace MultiLanguage;

public abstract class MultiLanguageApplicationTestBase<TStartupModule> : MultiLanguageTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
