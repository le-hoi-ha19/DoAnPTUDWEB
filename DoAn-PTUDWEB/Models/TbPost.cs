﻿using System;
using System.Collections.Generic;

namespace DoAn_PTUDWEB.Models
{
    public partial class TbPost
    {
        public TbPost()
        {
            TbPostComments = new HashSet<TbPostComment>();
        }

        public int BlogId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Detail { get; set; }
        public string? Image { get; set; }
        public string? SeoTitle { get; set; }
        public string? SeoDescription { get; set; }
        public string? SeoKeywords { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public int? UserId { get; set; }
        public bool? IsActive { get; set; }

        public virtual TbUser? User { get; set; }
        public virtual ICollection<TbPostComment> TbPostComments { get; set; }
    }
}
