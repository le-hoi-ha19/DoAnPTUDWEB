﻿using System;
using System.Collections.Generic;

namespace DoAn_PTUDWEB.Models
{
    public partial class TbRole
    {
        public TbRole()
        {
            TbUsers = new HashSet<TbUser>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<TbUser> TbUsers { get; set; }
    }
}
