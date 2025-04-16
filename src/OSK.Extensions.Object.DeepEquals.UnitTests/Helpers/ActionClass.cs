using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSK.Extensions.Object.DeepEquals.UnitTests.Helpers
{
    public class ActionClass
    {
        public Func<MockStruct, Task> Action { get; set; }

        public Action<MockStruct> Action2 { get; set; }

        public int this[string name] => 1;
    }
}
