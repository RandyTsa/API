using Pazzo.Common.Msg;
using Pazzo.Interface;
using Pazzo.Interface.Request;
using Pazzo.Interface.Response;
using Pazzo.Repository.Models;
using Pazzo.Repository.Repositories;
using System;
using System.Threading.Tasks;

namespace Pazzo.Service
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository memberRepository;

        public MemberService(IMemberRepository memberRepository)
        {
            this.memberRepository = memberRepository;
        }

        public async Task<ApplicationResult<CreateMemberResp>> CreateMemberAsync(CreateMemberReq req)
        {
            var entity = new Member() { IdNumber = req.IdNumber, Name = req.Name };

            var effectRows = await memberRepository.CreateByDapperAsync(entity);

            if (effectRows > 0)
            {
                var resp = new CreateMemberResp() { MemberId = effectRows };

                return ApplicationResult<CreateMemberResp>.Successed(resp);
            }

            return ApplicationResult<CreateMemberResp>.Failed(new ApplicationError() { Code = ReturnCodes.CODE_FAILURE, Description = MsgCodes.Msg_99 });
        }
    }
}