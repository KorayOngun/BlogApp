using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Results
{
    public class ErrorDataResult<T>:DataResult<T>
    {
        public ErrorDataResult(T data) : base(true, data)
        {

        }
        public ErrorDataResult(T data, string message) : base(true, data, message)
        {

        }
    }
}
