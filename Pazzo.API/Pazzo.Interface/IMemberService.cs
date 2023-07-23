using Pazzo.Interface.Request;
using Pazzo.Interface.Response;
using Pazzo.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pazzo.Interface
{
    public interface IMemberService
    {
        /// <summary>
        /// 新增會員
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<ApplicationResult<CreateMemberResp>> CreateAsync(CreateMemberReq req);

        /// <summary>
        /// 更新會員
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<ApplicationResult<UpdateMemberResp>> UpdateAsync(UpdateMemberReq req);

        /// <summary>
        /// 刪除會員
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<ApplicationResult<bool>> DeleteAsync(DeleteMemberReq req);

        /// <summary>
        /// 查詢單筆會員
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<ApplicationResult<QueryMemberResp>> QueryAsync(QueryMemberReq req);
    }
}