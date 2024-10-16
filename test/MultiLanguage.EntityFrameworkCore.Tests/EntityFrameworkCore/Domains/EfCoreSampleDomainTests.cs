using MultiLanguage.Samples;
using Xunit;

namespace MultiLanguage.EntityFrameworkCore.Domains;

[Collection(MultiLanguageTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<MultiLanguageEntityFrameworkCoreTestModule>
{

}
