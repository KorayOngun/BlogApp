using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Results
{
    public interface IResult
    {
        bool result { get; }
        string message { get; }

    }
    public class Result : IResult
    {
        public Result(bool _result)
        {
            result = _result;
        }
        public Result(string _message,bool _result):this(_result)
        {
            message = _message;
        }
        public bool result { get; }

        public string message { get; }
    }
}
