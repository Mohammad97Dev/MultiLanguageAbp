using MultiLanguage.Localization;
using Volo.Abp.Application.Services;

namespace MultiLanguage;

/* Inherit your application services from this class.
 */
public abstract class MultiLanguageAppService : ApplicationService
{
    protected MultiLanguageAppService()
    {
        LocalizationResource = typeof(MultiLanguageResource);
    }
}
