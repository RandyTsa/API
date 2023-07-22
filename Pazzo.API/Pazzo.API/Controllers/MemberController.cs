using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pazzo.Interface;
using Pazzo.Interface.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pazzo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : BaseApiController
    {
        private readonly IMemberService memberService;

        public MemberController(IMemberService memberService)
        {
            this.memberService = memberService;
        }

        //public async Task<ResponseResult<CreateMemberResp>> Create()
        //{
        //    await this.memberService.
        //}
    }
}