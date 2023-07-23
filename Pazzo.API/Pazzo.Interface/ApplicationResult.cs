using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pazzo.Interface
{
    /// <summary>
    /// Service層 統一回傳格式
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApplicationResult<T>
    {
        private readonly List<ApplicationError> _errors = new List<ApplicationError>();

        public bool Succeeded { get; protected set; }

        /// <summary>
        /// 處理錯誤訊息
        /// </summary>
        public IEnumerable<ApplicationError> Errors => _errors;

        /// <summary>
        /// 回傳資料
        /// </summary>
        public T Data { get; set; }

        public static ApplicationResult<T> Success => new ApplicationResult<T> { Succeeded = true };

        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="data">回傳資料</param>
        /// <returns></returns>
        public static ApplicationResult<T> Successed(T data)
        {
            var result = new ApplicationResult<T> { Succeeded = true };
            result.Data = data;

            return result;
        }

        /// <summary>
        /// 失敗
        /// </summary>
        /// <param name="errors">錯誤訊息</param>
        /// <returns></returns>
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