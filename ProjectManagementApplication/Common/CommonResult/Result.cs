
namespace ProjectManagementApplication.Common.Results
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public bool IsFailiar => !IsSuccess;
        public Error error { get; set; } = default!;
        public Result(bool suc, Error err)
        {
            if ((err == Error.None && !suc) || (err != Error.None && suc))
            {
                throw new InvalidOperationException();
            }
            IsSuccess = suc;
            error = err;
        }
        public static Result Success() => new Result(true, Error.None);
        public static Result Fail(Error err) => new Result(false, err);
        public static Result<T> Success<T>(T val) => new(true, Error.None, val);
        public static Result<T> Fail<T>(Error err) => new(false, err, default!);

    }
}
