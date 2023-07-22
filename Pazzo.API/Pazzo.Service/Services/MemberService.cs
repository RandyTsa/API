using Pazzo.Interface;
using Pazzo.Interface.Request;
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

        public async Task<ApplicationResult<Member>> CreateMemberAsync(CreateMemberReq req)
        {
            var isParse = int.TryParse(req.IdNumber, out int id);

            if (!isParse)
            {
                throw new Exception("Id 只允許為數字");
            }
            var entity = new Member() { IdNumber = req.IdNumber, Name = req.Name };

            var effectRows = await memberRepository.CreateByDapperAsync(entity);

            return effectRows > 0 ? ApplicationResult<Member>.Success : ApplicationResult<Member>.Failed();
        }
    }
}