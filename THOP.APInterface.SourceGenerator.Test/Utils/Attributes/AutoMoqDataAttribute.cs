using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace THop.APInterface.SourceGenerator.Test.Utils.Attributes
{
    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute() : base(() => new Fixture().Customize(new AutoMoqCustomization()))
        {

        }
    }
}
