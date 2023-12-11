using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAn_PTUDWEB.Models
{
    public class TbProductColor
    {
        public int ProductColorId { get; set; }
        [ForeignKey("TbColor")]
        public int ColorId { get; set; }
        public virtual TbColor Color { get; set; } = null!;
        [ForeignKey("TbProduct")]
        public int ProductId { get; set; }
        public virtual TbProduct Product { get; set; } = null!;
        public string? Description { get; set; }
    }
}
