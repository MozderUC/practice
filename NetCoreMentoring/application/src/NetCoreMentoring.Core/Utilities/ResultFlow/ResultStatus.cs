namespace NetCoreMentoring.Core.Utilities.ResultFlow
{
    public enum ResultStatus
    {
        /// Status that indicates a successful execution.
        Success = 0,
        /// Status that indicates a validation error.
        ValidationError = 1,
        /// Status for empty search result.
        NotFound = 2,
        /// Status that indicates a irreparable execution error.
        Failed = 100,
    }
}
