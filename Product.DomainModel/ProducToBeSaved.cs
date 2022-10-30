using System;
using System.Collections.Generic;
using System.Text;

namespace Product.Domain.Model
{
    public class ProducToBeSaved
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string ImgURL { get; set; }
        public int CategoryID { get; set; }

    }
}
