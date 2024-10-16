using MultiLanguage.Samples;
using Xunit;

namespace MultiLanguage.EntityFrameworkCore.Applications;

[Collection(MultiLanguageTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<MultiLanguageEntityFrameworkCoreTestModule>
{

}
