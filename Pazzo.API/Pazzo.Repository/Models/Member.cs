using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Pazzo.Repository.Models
{
    [Table("Member")]
    public partial class Member
    {
        [Key]
        public int MemberId { get; set; }

        [StringLength(10)]
        public string IdNumber { get; set; }

        [StringLength(10)]
        public string Name { get; set; }
    }
}