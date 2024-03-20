using System;
using System.Collections.Generic;
using System.Text;

namespace OSK.Extensions.Object.DeepEquals.Options
{
    public class ExecutionOptions
    {
        public bool PreserveCacheBetweenConfigurationChanges { get; set; }

        public bool ThrowOnFailure { get; set; }
    }
}
