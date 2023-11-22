using System;
using System.Collections.Generic;

namespace DoAn_PTUDWEB.Models
{
    public partial class TbSlide
    {
        public TbSlide()
        {
            TbImageSlides = new HashSet<TbImageSlide>();
        }

        public int SlideId { get; set; }
        public string? Caption { get; set; }
        public string? Summary { get; set; }
        public string? Link { get; set; }

        public virtual ICollection<TbImageSlide> TbImageSlides { get; set; }
    }
}
