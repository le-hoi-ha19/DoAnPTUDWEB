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
            TbReviews = new HashSet<TbReview>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string? Avatar { get; set; }
        public string PasswordHash { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string Email { get; set; } = null!;
        public bool? Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int RoleId { get; set; }

        public virtual TbRole Role { get; set; } = null!;
        public virtual ICollection<TbOrder> TbOrders { get; set; }
        public virtual ICollection<TbPost> TbPosts { get; set; }
        public virtual ICollection<TbReview> TbReviews { get; set; }
    }
}
