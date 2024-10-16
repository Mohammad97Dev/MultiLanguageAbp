using Volo.Abp.Settings;

namespace MultiLanguage.Settings;

public class MultiLanguageSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(MultiLanguageSettings.MySetting1));
    }
}
