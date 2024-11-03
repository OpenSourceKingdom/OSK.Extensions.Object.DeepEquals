namespace OSK.Extensions.Object.DeepEquals.Options
{
    public class ExecutionOptions
    {
        /// <summary>
        /// Determines if the caches tracking reflection data (property infos, etc.) should be cleared when updates to the global comparison <see cref="DeepEqualsConfiguration"/> are applied by option overrides
        /// </summary>
        public bool PreserveCacheBetweenConfigurationChanges { get; set; }

        /// <summary>
        /// Determines if the DeepEqual comparison should throw a <see cref="Models.DeepEqualityComparisonFailedException"/> on a comparison failure.
        /// </summary>
        public bool ThrowOnFailure { get; set; }
    }
}
