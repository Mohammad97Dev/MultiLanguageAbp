using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace MultiLanguage;

[Dependency(ReplaceServices = true)]
public class MultiLanguageBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "MultiLanguage";
}
