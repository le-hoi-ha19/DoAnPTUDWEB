using System;
using System.Collections.Generic;

namespace DoAn_PTUDWEB.Models
{
    public partial class TbType
    {
        public TbType()
        {
            TbOrderDetails = new HashSet<TbOrderDetail>();
            TbTypeProducts = new HashSet<TbTypeProduct>();
        }

        public int TypeId { get; set; }
        public string? TypeName { get; set; }

        public virtual ICollection<TbOrderDetail> TbOrderDetails { get; set; }
        public virtual ICollection<TbTypeProduct> TbTypeProducts { get; set; }
    }
}
