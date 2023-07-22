using Dapper;
using Pazzo.Common.DataAccessLayer;
using Pazzo.Repository.Contexts;
using Pazzo.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pazzo.Repository.Repositories
{
    public class MemberRepository : BaseRepository<Member>, IMemberRepository
    {
        private readonly PazzoContext pazzoContext;

        public MemberRepository(PazzoContext pazzoContext) : base(pazzoContext)
        {
            this.pazzoContext = pazzoContext;
        }

        public async Task<int> CreateByDapperAsync(Member member)
        {
            var sql = @"INSERT INTO pazzo.member (Id, Name)
VALUES (@Id, @Name);
";
            using (var conn = base.GetConnection())
            {
                return await conn.ExecuteAsync(sql, new
                {
                    Id = member.Id,
                    Name = member.Name,
                });
            }
        }
    }

    public interface IMemberRepository : IBaseRepository<Member>
    {
        Task<int> CreateByDapperAsync(Member member);
    }
}