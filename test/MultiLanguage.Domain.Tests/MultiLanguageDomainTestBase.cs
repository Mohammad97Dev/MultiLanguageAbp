using Volo.Abp.Modularity;

namespace MultiLanguage;

/* Inherit from this class for your domain layer tests. */
public abstract class MultiLanguageDomainTestBase<TStartupModule> : MultiLanguageTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
