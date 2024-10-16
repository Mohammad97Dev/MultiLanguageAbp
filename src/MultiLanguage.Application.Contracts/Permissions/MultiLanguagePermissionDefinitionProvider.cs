using MultiLanguage.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace MultiLanguage.Permissions;

public class MultiLanguagePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(MultiLanguagePermissions.GroupName);

        //Define your own permissions here. Example:
        //myGroup.AddPermission(MultiLanguagePermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<MultiLanguageResource>(name);
    }
}
