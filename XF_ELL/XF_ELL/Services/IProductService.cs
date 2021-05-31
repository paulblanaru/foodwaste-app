using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XF_ELL.DTO;

namespace XF_ELL.Services
{
    public interface IProductService
    {
             Task<ProductDTO> GetById(int id);
            Task<IEnumerable<ProductDTO>> GetAll();
            Task<ProductDTO> Save(ProductDTO product);
           // Task Remove(int id);
            Task<ProductDTO> Update(int id, ProductDTO product);
    }
}
