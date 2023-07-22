using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pazzo.Service
{
    public class ApplicationResult<T>
    {
        private readonly List<ApplicationError> _errors = new List<ApplicationError>();

        public bool Succeeded { get; protected set; }

        public IEnumerable<ApplicationError> Errors => _errors;

        public T Data { get; set; }

        public static ApplicationResult<T> Success => new ApplicationResult<T> { Succeeded = true };

        public static ApplicationResult<T> Successed(T data)
        {
            var result = new ApplicationResult<T> { Succeeded = true };
            result.Data = data;

            return result;
        }

        public static ApplicationResult<T> Failed(params ApplicationError[] errors)
        {
            var result = new ApplicationResult<T> { Succeeded = false };
            if (errors != null)
            {
                result._errors.AddRange(errors);
            }
            return result;
        }

        public string FailedMessage()
        {
            return string.Join(", ", Errors.Select(x => x.Description));
        }
    }
}