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

        public async Task<ApplicationResult<CreateMemberResp>> CreateAsync(CreateMemberReq req)
        {
            var entity = new Member() { IdNumber = req.IdNumber, Name = req.Name };

            var newId = await memberRepository.CreateByDapperAsync(entity);

            if (newId > 0)
            {
                return ApplicationResult<CreateMemberResp>.Successed(new CreateMemberResp() { MemberId = newId });
            }

            return ApplicationResult<CreateMemberResp>.Failed(new ApplicationError() { Code = ReturnCodes.CODE_FAILURE, Description = MsgCodes.Msg_99 });
        }

        public async Task<ApplicationResult<UpdateMemberResp>> UpdateAsync(UpdateMemberReq req)
        {
            try
            {
                var entity = new Member()
                {
                    MemberId = req.MemberId,
                    IdNumber = req.IdNumber,
                    Name = req.Name,
                };
                var member = await memberRepository.UpdateByDapperAsync(entity);

                if (member != null)
                {
                    return ApplicationResult<UpdateMemberResp>.Successed(new UpdateMemberResp() { MemberId = member.MemberId, Name = member.Name, IdNumber = member.IdNumber });
                }

                return ApplicationResult<UpdateMemberResp>.Failed(new ApplicationError() { Code = ReturnCodes.CODE_FAILURE, Description = MsgCodes.Msg_01 });
            }
            catch (Exception ex)
            {
                return ApplicationResult<UpdateMemberResp>.Failed(new ApplicationError() { Code = ReturnCodes.CODE_FAILURE, Description = ex.Message });
            }
        }

        public async Task<ApplicationResult<bool>> DeleteAsync(DeleteMemberReq req)
        {
            try
            {
                var effectRows = await memberRepository.DeleteByDapperAsync(req.MemberId);

                if (effectRows > 0)
                {
                    return ApplicationResult<bool>.Successed(true);
                }

                return ApplicationResult<bool>.Failed(new ApplicationError() { Code = ReturnCodes.CODE_FAILURE, Description = MsgCodes.Msg_01 });
            }
            catch (Exception ex)
            {
                return ApplicationResult<bool>.Failed(new ApplicationError() { Code = ReturnCodes.CODE_FAILURE, Description = ex.Message });
            }
        }
    }
}