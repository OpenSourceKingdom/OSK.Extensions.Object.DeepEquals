namespace OSK.Extensions.Object.DeepEquals.Options
{
    public class EnumerableComparisonOptions
    {
        /// <summary>
        /// If true, the ordering of sequences between objects will be validated. 
        /// That is, if set to true, sequences [ A, B ] and [ B, A ] will return false.
        /// 
        /// Note: this may reduce performance 
        /// </summary>
        public bool EnforceEnumerableOrdering { get; set; }
    }
}
