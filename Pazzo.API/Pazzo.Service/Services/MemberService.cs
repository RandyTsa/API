using Pazzo.Interface;
using Pazzo.Repository.Repositories;
using System;

namespace Pazzo.Service
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository memberRepository;

        public MemberService()
        {
        }
    }
}