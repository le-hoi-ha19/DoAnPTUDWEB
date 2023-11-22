using System;
using System.Collections.Generic;

namespace DoAn_PTUDWEB.Models
{
    public partial class TbImageSlide
    {
        public int ImageSlideId { get; set; }
        public string? File { get; set; }
        public int? SlideId { get; set; }

        public virtual TbSlide? Slide { get; set; }
    }
}
