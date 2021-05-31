using System;
using System.Collections.Generic;
using System.Text;

namespace XF_ELL.Results
{
    public class CreateProductResult
    {
        public string NewName { get; set; }
        public DateTime NewExpirationDate { get; set; }
        public bool IsAdded { get; set; }
        public byte[] NewProductPhotoBytes { get; set; }
    }
}
