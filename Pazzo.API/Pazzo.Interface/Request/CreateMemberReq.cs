using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pazzo.Interface.Request
{
    public class CreateMemberReq
    {
        /// <summary>
        /// 身分證
        /// </summary>
        public string IdNumber { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
    }
}