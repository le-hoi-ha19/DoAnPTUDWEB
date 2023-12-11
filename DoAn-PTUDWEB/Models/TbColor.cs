using System;
using System.Collections.Generic;

namespace DoAn_PTUDWEB.Models
{
    public class TbColor
    {
        public TbColor()
        {
            TbProductColors = new HashSet<TbProductColor>();
        }

        public int ColorId { get; set; }
        public string ColorName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public virtual ICollection<TbProductColor> TbProductColors { get; set; }
    }
}
