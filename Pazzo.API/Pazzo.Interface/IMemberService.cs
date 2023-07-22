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
        Task<ApplicationResult<CreateMemberResp>> CreateMemberAsync(CreateMemberReq req);
    }
}