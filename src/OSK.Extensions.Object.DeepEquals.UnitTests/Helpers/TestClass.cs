using System.Collections.Generic;

namespace OSK.Extensions.Object.DeepEquals.UnitTests.Helpers
{
    public class TestClass
    {
        public string A { get; set; }

        public int B { get; set; }

        public List<int> Ints { get; set; }

        public TestClass SubClass { get; set; }
    }
}
