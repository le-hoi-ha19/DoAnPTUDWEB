using System;
using System.Collections.Generic;

namespace DoAn_PTUDWEB.Models
{
    public partial class TbReview
    {
        public int ReviewId { get; set; }
        public int? ProductId { get; set; }
        public int? UserId { get; set; }
        public int? Rating { get; set; }
        public string? Comment { get; set; }
        public string? CreatedDate { get; set; }

        public virtual TbUser? Product { get; set; }
        public virtual TbProduct Review { get; set; } = null!;
    }
}
