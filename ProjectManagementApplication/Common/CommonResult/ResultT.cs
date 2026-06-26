using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementApplication.Common.Results
{
    public class Result<T> : Result
    {
        readonly T _value;
        public Result(bool suc, Error err, T val) : base(suc, err)
        {
            _value = val;
        }
        public T Value => IsSuccess ? _value : throw new InvalidOperationException();
    }
}
