using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace ProjectManagementApplication.Common.Results
{
    public class Result
    {
        public bool Success { get; set; }
        public bool Failiar => !Success;
        public Error error { get; set; } = default!;
        public Result(bool suc, Error err)
        {
            if ((err == Error.None && !suc) || (err != Error.None && suc))
            {
                throw new InvalidOperationException();
            }
            Success = suc;
            error = err;
        }
        public static Result IsSuccess() => new Result(true, Error.None);
        public static Result IsFail(Error err) => new Result(false, err);
        public static Result<T> IsSuccess<T>(T val) => new(true, Error.None, val);
        public static Result<T> IsFail<T>(Error err) => new(false, err, default!);

    }
}
