using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MultiLanguage.Data;
using Volo.Abp.DependencyInjection;

namespace MultiLanguage.EntityFrameworkCore;

public class EntityFrameworkCoreMultiLanguageDbSchemaMigrator
    : IMultiLanguageDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreMultiLanguageDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the MultiLanguageDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<MultiLanguageDbContext>()
            .Database
            .MigrateAsync();
    }
}
