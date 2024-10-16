using System.Threading.Tasks;

namespace MultiLanguage.Data;

public interface IMultiLanguageDbSchemaMigrator
{
    Task MigrateAsync();
}
