using System;
using System.Collections.Generic;

namespace DoAn_PTUDWEB.Models
{
    public partial class TbReview
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int? Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual TbProduct Product { get; set; } = null!;
        public virtual TbUser User { get; set; } = null!;
    }
}
