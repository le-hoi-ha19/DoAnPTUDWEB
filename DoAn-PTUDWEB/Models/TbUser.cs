using System;
using System.Collections.Generic;

namespace DoAn_PTUDWEB.Models
{
    public partial class TbUser
    {
        public TbUser()
        {
            TbOrders = new HashSet<TbOrder>();
            TbPosts = new HashSet<TbPost>();
        }

        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? FullName { get; set; }
        public string? PasswordHash { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public int? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? Deleted { get; set; }
        public int? RoleId { get; set; }

        public virtual TbRole? Role { get; set; }
        public virtual ICollection<TbOrder> TbOrders { get; set; }
        public virtual ICollection<TbPost> TbPosts { get; set; }
    }
}
