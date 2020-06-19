using System.Threading.Tasks;

namespace DotNetCore.Results
{
    public class Result : IResult
    {
        protected Result() { }

        public bool Failed => !Succeeded;

        public string Message { get; protected set; }

        public bool Succeeded { get; protected set; }

        public static IResult Fail()
        {
            return new Result { Succeeded = false };
        }

        public static IResult Fail(string message)
        {
            return new Result { Succeeded = false, Message = message };
        }

        public static Task<IResult> FailAsync()
        {
            return Task.FromResult(Fail());
        }

        public static Task<IResult> FailAsync(string message)
        {
            return Task.FromResult(Fail(message));
        }

        public static IResult Success()
        {
            return new Result { Succeeded = true };
        }

        public static IResult Success(string message)
        {
            return new Result { Succeeded = true, Message = message };
        }

        public static Task<IResult> SuccessAsync()
        {
            return Task.FromResult(Success());
        }

        public static Task<IResult> SuccessAsync(string message)
        {
            return Task.FromResult(Success(message));
        }
    }

    public class Result<T> : Result, IResult<T>
    {
        protected Result() { }

        public T Data { get; private set; }

        public static new IResult<T> Fail()
        {
            return new Result<T> { Succeeded = false };
        }

        public static new IResult<T> Fail(string message)
        {
            return new Result<T> { Succeeded = false, Message = message };
        }

        public static new Task<IResult<T>> FailAsync()
        {
            return Task.FromResult(Fail());
        }

        public static new Task<IResult<T>> FailAsync(string message)
        {
            return Task.FromResult(Fail(message));
        }

        public static new IResult<T> Success()
        {
            return new Result<T> { Succeeded = true };
        }

        public static new IResult<T> Success(string message)
        {
            return new Result<T> { Succeeded = true, Message = message };
        }

        public static IResult<T> Success(T data)
        {
            return new Result<T> { Succeeded = true, Data = data };
        }

        public static IResult<T> Success(T data, string message)
        {
            return new Result<T> { Succeeded = true, Data = data, Message = message };
        }

        public static new Task<IResult<T>> SuccessAsync()
        {
            return Task.FromResult(Success());
        }

        public static new Task<IResult<T>> SuccessAsync(string message)
        {
            return Task.FromResult(Success(message));
        }

        public static Task<IResult<T>> SuccessAsync(T data)
        {
            return Task.FromResult(Success(data));
        }

        public static Task<IResult<T>> SuccessAsync(T data, string message)
        {
            return Task.FromResult(Success(data, message));
        }
    }
}
