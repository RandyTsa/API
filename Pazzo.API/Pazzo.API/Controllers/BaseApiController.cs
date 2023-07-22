using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pazzo.Common.Msg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pazzo.API.Controllers
{
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected OkObjectResult Success(string message = MsgCodes.Msg_00)
        {
            return Ok(new ResponseResult() { RtnCode = ReturnCodes.CODE_SUCCESS, Msg = message });
        }

        protected OkObjectResult Success<T>(T content, string message = MsgCodes.Msg_00)
        {
            return Ok(new ResponseResult<T>() { RtnCode = ReturnCodes.CODE_SUCCESS, Msg = message, Data = content });
        }

        protected ResponseResult<T> SuccessResult<T>(T content, string message = MsgCodes.Msg_00)
        {
            return new ResponseResult<T>() { RtnCode = ReturnCodes.CODE_SUCCESS, Msg = message, Data = content };
        }

        protected ResponseResult SuccessResult(string message = MsgCodes.Msg_00)
        {
            return new ResponseResult() { RtnCode = ReturnCodes.CODE_SUCCESS, Msg = message };
        }

        protected OkObjectResult Failure(string message = MsgCodes.Msg_99)
        {
            return Ok(new ResponseResult() { RtnCode = ReturnCodes.CODE_FAILURE, Msg = message });
        }

        protected OkObjectResult Failure<T>(T content, string message = MsgCodes.Msg_99)
        {
            return Ok(new ResponseResult<T>() { RtnCode = ReturnCodes.CODE_FAILURE, Msg = message, Data = content });
        }
    }

    public class ResponseResult
    {
        /// <summary>
        /// 0:成功, 非0:失敗
        /// </summary>
        public int RtnCode { get; set; }

        public string Msg { get; set; }
    }

    public class ResponseResult<T>
    {
        /// <summary>
        /// 0:成功, 非0:失敗
        /// </summary>
        public int RtnCode { get; set; }

        public string Msg { get; set; }

        public T Data { get; set; }
    }
}