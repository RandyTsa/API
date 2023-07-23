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

        /// <summary>
        /// Insert method.
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public async Task<int> CreateByDapperAsync(Member member)
        {
            var sql = @"INSERT INTO Member (IdNumber, Name)
OUTPUT INSERTED.MemberId
VALUES (@IdNumber, @Name);
";
            using (var conn = base.GetConnection())
            {
                var newId = await conn.QueryAsync<int>(sql, new
                {
                    IdNumber = member.IdNumber,
                    Name = member.Name,
                });
                return newId.FirstOrDefault();
            }
        }

        /// <summary>
        /// Update method.
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public async Task<Member> UpdateByDapperAsync(Member member)
        {
            var sql = @"UPDATE Member
SET Name = @Name, IdNumber = @IdNumber
WHERE MemberId = @MemberId;

Select * FROM Member Where MemberId = @MemberId
";

            using (var conn = base.GetConnection())
            {
                var result = await conn.QueryAsync<Member>(sql, new
                {
                    IdNumber = member.IdNumber,
                    Name = member.Name,
                    MemberId = member.MemberId,
                });

                return result.FirstOrDefault();
            }
        }

        /// <summary>
        /// Delete method.
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public async Task<int> DeleteByDapperAsync(int memberId)
        {
            var sql = @"DELETE FROM Member
WHERE MemberId = @MemberId;
";
            using (var conn = base.GetConnection())
            {
                return await conn.ExecuteAsync(sql, new
                {
                    MemberId = memberId,
                });
            }
        }

        /// <summary>
        /// Query method.
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public async Task<Member> QueryByDapperAsync(int memberId)
        {
            var sql = @"SELECT * FROM Member
WHERE MemberId = @MemberId;
";
            using (var conn = base.GetConnection())
            {
                var member = await conn.QueryAsync<Member>(sql, new
                {
                    MemberId = memberId,
                });

                return member.FirstOrDefault();
            }
        }
    }

    public interface IMemberRepository : IBaseRepository<Member>
    {
        Task<int> CreateByDapperAsync(Member member);

        Task<Member> UpdateByDapperAsync(Member member);

        Task<int> DeleteByDapperAsync(int memberId);

        Task<Member> QueryByDapperAsync(int memberId);
    }
}