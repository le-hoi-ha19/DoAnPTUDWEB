using System;
using System.Collections.Generic;

namespace DoAn_PTUDWEB.Models
{
    public partial class TbTuKhoaSanPham
    {
        public int MaTksp { get; set; }
        public int? MaTk { get; set; }
        public int? ProductId { get; set; }

        public virtual TbTuKhoa? MaTkNavigation { get; set; }
        public virtual TbProduct? Product { get; set; }
    }
}
