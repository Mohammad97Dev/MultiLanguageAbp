using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace MultiLanguage.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class MultiLanguageDbContextFactory : IDesignTimeDbContextFactory<MultiLanguageDbContext>
{
    public MultiLanguageDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();
        
        MultiLanguageEfCoreEntityExtensionMappings.Configure();

        var builder = new DbContextOptionsBuilder<MultiLanguageDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));
        
        return new MultiLanguageDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../MultiLanguage.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
