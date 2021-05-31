using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XF_ELL.Results
{
    public class EditProductResult
    {
        public bool Edited { get; set; }
        public string NewName { get; set; }
        public int NewQuantity { get; set; }
        public string NewPostedDate { get; set; }
        public string NewExpirationDate { get; set; }
        public ImageSource NewProductImageSource { get; set; }
    }
}

