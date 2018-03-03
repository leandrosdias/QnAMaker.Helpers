using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QnAMaker.Helpers.Models;

namespace QnAMaker.Helpers.Helpers
{
    public class ResultHelper
    {
        public static Result GetError(Result result)
        {
            result.Sucess = false;
            return result;
        }
        public static Result GetGenericError(string erroMessage)
        {
            return new Result
            {
                Sucess = false,
                Error = new Error{Code = "X", Message = erroMessage}
            };
        }
        public static Result GetSucess(string message)
        {
            return new Result
            {
                Sucess = true,
                Error = new Error{Code = string.Empty, Message = message}
            };
        }
    }
}
