using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace MultiLanguage.Data;

/* This is used if database provider does't define
 * IMultiLanguageDbSchemaMigrator implementation.
 */
public class NullMultiLanguageDbSchemaMigrator : IMultiLanguageDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
