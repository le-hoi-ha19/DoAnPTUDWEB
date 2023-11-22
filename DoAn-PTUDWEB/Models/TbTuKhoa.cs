using System;
using System.Collections.Generic;

namespace DoAn_PTUDWEB.Models
{
    public partial class TbTuKhoa
    {
        public TbTuKhoa()
        {
            TbTuKhoaSanPhams = new HashSet<TbTuKhoaSanPham>();
        }

        public int MaTk { get; set; }
        public string? Name { get; set; }
        public string? Mota { get; set; }

        public virtual ICollection<TbTuKhoaSanPham> TbTuKhoaSanPhams { get; set; }
    }
}
