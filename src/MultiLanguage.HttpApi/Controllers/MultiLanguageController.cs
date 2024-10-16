using MultiLanguage.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace MultiLanguage.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class MultiLanguageController : AbpControllerBase
{
    protected MultiLanguageController()
    {
        LocalizationResource = typeof(MultiLanguageResource);
    }
}
