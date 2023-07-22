﻿using Pazzo.Common.DataAccessLayer;
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
    }

    public interface IMemberRepository : IBaseRepository<Member>
    {
    }
}