using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Results
{
    public interface IDataResult<T> : IResult
    {
        T Data { get; }
    }
    public class DataResult<T> : Result, IDataResult<T> 
    {
        public DataResult(bool result,T data):base(result)
        {
            Data = data;
        }
        public DataResult(bool result,T data, string message) :base(message,result)
        {
            Data = data;
        }
        public T Data { get; }
    }
}
