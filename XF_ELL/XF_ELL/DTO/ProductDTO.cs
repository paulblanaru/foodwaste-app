using System;
using System.Collections.Generic;
using System.Text;

namespace XF_ELL.DTO
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public DateTime PostedDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string AsignedUser { get; set; }
        public string EncodedImage { get; set; }
    }
}
