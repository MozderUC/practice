using System;

namespace NetCoreMentoring.Core.Utilities.ResultFlow
{
    /// Structure that implements the status of the operation.
    public readonly struct Result
    {
        private readonly Error _error;

        public Result(ResultStatus status)
        {
            this.Status = status;
            this._error = new Error();
        }

        public Result(ResultStatus status, Error error)
        {
            this.Status = status;
            this._error = error;
        }

        public bool IsSuccess => this.Status == ResultStatus.Success;

        public Error Error
        {
            get
            {
                if (this.IsSuccess)
                    throw new InvalidOperationException("The error property for a successful result is empty.");
                return this._error;
            }
        }

        public ResultStatus Status { get; }

        public static Result ValidationError(Error error)
        {
            return new Result(ResultStatus.ValidationError, error);
        }

        public static Result<T> ValidationError<T>(Error error)
        {
            return new Result<T>(ResultStatus.ValidationError, error, default(T));
        }

        public static Result Failure(Error error)
        {
            return new Result(ResultStatus.Failed, error);
        }

        public static Result<T> Failure<T>(Error error)
        {
            return new Result<T>(ResultStatus.Failed, error, default(T));
        }

        public static Result NotFound(Error error)
        {
            return new Result(ResultStatus.NotFound, error);
        }

        public static Result<T> NotFound<T>(Error error)
        {
            return new Result<T>(ResultStatus.NotFound, error, default(T));
        }

        public static Result Success()
        {
            return new Result(ResultStatus.Success, new Error());
        }

        public static Result<T> Success<T>(T value)
        {
            return new Result<T>(ResultStatus.Success, new Error(), value);
        }
    }
}
