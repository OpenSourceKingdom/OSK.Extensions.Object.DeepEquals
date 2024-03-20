using System;

namespace OSK.Extensions.Object.DeepEquals.Models
{
    public class DeepEqualityComparisonFailedException: Exception
    {
        public DeepEqualityComparisonFailedException(string message) 
            : base(message) { }
    }
}
