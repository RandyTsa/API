using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pazzo.Common.Msg;
using Pazzo.Interface.Request;
using Pazzo.Repository.Models;
using Pazzo.Repository.Repositories;
using Pazzo.Service;
using Xunit;

namespace Pazzo.UnitTest
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public async Task Create_Success()
        {
            // Arrange
            var req = new CreateMemberReq
            {
                IdNumber = "A123456789",
                Name = "Randy"
            };

            // 模擬成功後回傳的Key
            var newId = 999;

            // 創建Mock Repository & Service 並塞入預設key
            var memberRepositoryMock = new Mock<IMemberRepository>();
            memberRepositoryMock.Setup(repo => repo.CreateByDapperAsync(It.IsAny<Member>()))
                                .ReturnsAsync(newId);
            var memberService = new MemberService(memberRepositoryMock.Object);

            // Act
            var result = await memberService.CreateAsync(req);

            // Assert
            // 验证方法的返回值是否正确
            Assert.IsTrue(result.Succeeded);
            Assert.AreEqual(newId, result.Data.MemberId);
        }

        [TestMethod]
        public async Task Create_Fail()
        {
            // Arrange
            var req = new CreateMemberReq
            {
                IdNumber = "A123456789",
                Name = "Randy"
            };

            // 模擬失敗情境回傳 ReturnCode = 0
            var memberRepositoryMock = new Mock<IMemberRepository>();
            memberRepositoryMock.Setup(repo => repo.CreateByDapperAsync(It.IsAny<Member>()))
                                .ReturnsAsync(0);

            var memberService = new MemberService(memberRepositoryMock.Object);

            // Act
            var result = await memberService.CreateAsync(req);

            // Assert
            Assert.IsFalse(result.Succeeded);
            Assert.AreEqual(ReturnCodes.CODE_FAILURE, result.Errors.FirstOrDefault().Code);
            Assert.AreEqual(MsgCodes.Msg_99, result.Errors.FirstOrDefault().Description);
        }
    }
}