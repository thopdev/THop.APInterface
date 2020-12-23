using AutoFixture.Xunit2;

namespace THop.APInterface.SourceGenerator.Test.Utils.Attributes
{
    public class InlineDomainDataAttribute : InlineAutoDataAttribute
    {
        public InlineDomainDataAttribute(params object[] values) : base(new AutoMoqDataAttribute(), values)
        {

        }
    }
}