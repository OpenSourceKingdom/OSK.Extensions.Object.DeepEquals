using OSK.Extensions.Object.DeepEquals.Internal.Services;
using OSK.Extensions.Object.DeepEquals.Ports;

namespace OSK.Extensions.Object.DeepEquals.UnitTests.Internal.Services
{
    public class DeepComparisonBuilderTests
    {
        #region Variables

        private IDeepComparisonBuilder _builder;

        #endregion

        #region Constructors

        public DeepComparisonBuilderTests()
        {
            _builder = new DeepComparisonBuilder();
        }

        #endregion
    }
}
