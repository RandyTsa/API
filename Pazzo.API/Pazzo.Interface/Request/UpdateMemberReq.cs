﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pazzo.Interface.Request
{
    public class UpdateMemberReq
    {
        public int MemberId { get; set; }

        public string Name { get; set; }

        public string IdNumber { get; set; }
    }
}