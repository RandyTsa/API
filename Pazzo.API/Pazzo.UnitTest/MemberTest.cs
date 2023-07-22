using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pazzo.Interface;
using Pazzo.Repository.Contexts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pazzo.Interface.Request;
using Pazzo.Repository.Repositories;

namespace Pazzo.UnitTest
{
    [TestClass]
    public class MemberTest : BaseTest
    {
        private IMemberService memberService;
        private IMemberRepository memberRepository;

        private PazzoContext dbContext;

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            this.memberService = base.provider.GetService<IMemberService>();
            this.memberRepository = base.provider.GetService<IMemberRepository>();

            this.dbContext = base.provider.GetService<PazzoContext>();
        }

        [TestMethod]
        public async Task CreateTest()
        {
            var req = new CreateMemberReq() { Name = "Randy", IdNumber = "A551122458" };

            var result = await memberService.CreateAsync(req);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Succeeded);
        }
    }
}