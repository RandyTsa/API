using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pazzo.Common.Msg;
using Pazzo.Interface;
using Pazzo.Interface.Request;
using Pazzo.Interface.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pazzo.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MemberController : BaseApiController
    {
        private readonly IMemberService memberService;

        public MemberController(IMemberService memberService)
        {
            this.memberService = memberService;
        }

        [HttpPost("Create")]
        public async Task<ResponseResult<CreateMemberResp>> Create(CreateMemberReq req)
        {
            var result = await this.memberService.CreateMemberAsync(req);
            if (result.Succeeded)
            {
                return new ResponseResult<CreateMemberResp>() { RtnCode = ReturnCodes.CODE_SUCCESS, Data = result.Data };
            }

            foreach (var error in result.Errors)
            {
                return new ResponseResult<CreateMemberResp>() { RtnCode = error.Code, Msg = error.Description };
            }

            return new ResponseResult<CreateMemberResp>();
        }
    }
}