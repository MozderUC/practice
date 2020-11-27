namespace NetCoreMentoring.Core.Utilities.ResultFlow
{
    public readonly struct Error
    {
        public Error(string message)
            :this(message, null)
        {
        }

        public Error(string message, object errorDetails)
        {
            this.Message = message;
            this.ErrorDetails = errorDetails;
        }

        public string Message { get; }
        public object ErrorDetails { get; }
    }
}
