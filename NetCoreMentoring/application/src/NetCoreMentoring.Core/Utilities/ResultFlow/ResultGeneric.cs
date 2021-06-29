using System;

namespace NetCoreMentoring.Core.Utilities.ResultFlow
{
    /// Generic structure that implements the status and the result of the operation.
    public readonly struct Result<T>
    {
        private readonly Result generalLogic;
        private readonly T value;

        internal Result(Result result)
        {
            this.generalLogic = result;
            this.value = default(T);
        }

        internal Result(ResultStatus status, Error error, T value)
        {
            if (status == ResultStatus.Success && object.Equals((object)value, (object)default(T)))
                status = ResultStatus.NotFound;
            this.generalLogic = new Result(status, error);
            this.value = value;
        }

        public bool IsSuccess => this.generalLogic.IsSuccess;

        public Error Error => this.generalLogic.Error;

        public ResultStatus Status => this.generalLogic.Status;

        public T Value
        {
            get
            {
                if (!this.IsSuccess)
                    throw new InvalidOperationException("A failed result has no value.");
                return this.value;
            }
        }

        public static implicit operator Result(Result<T> result)
        {
            return result.generalLogic;
        }
    }
}
