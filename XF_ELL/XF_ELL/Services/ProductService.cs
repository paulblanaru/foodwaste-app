using Firebase.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using XF_ELL.DTO;

namespace XF_ELL.Services
{
    public class ProductService : IProductService
    {
        private List<ProductDTO> _products = new List<ProductDTO>(){
            new ProductDTO()
            {
                 ProductId = 1,
                 Name = "Bananas",
                 Quantity = 2,
                 ExpirationDate = new DateTime(2021,4,11),
                 PostedDate = new DateTime(2021,3,11),
                 AsignedUser = "paul"
            },
            new ProductDTO()
            {
                 ProductId = 2,
                 Name = "Bread",
                 Quantity = 1,
                 ExpirationDate = new DateTime(2021,4,21),
                 PostedDate = new DateTime(2021,3,22),
                 AsignedUser = "peter"
            }
        };
        public ProductService()
        {
                
        }

        public async Task<IEnumerable<ProductDTO>> GetAll()
        {
            return _products;
        }

        public async Task<ProductDTO> GetById(int id)
        {
            return _products.FirstOrDefault(u => u.ProductId.Equals(id));
        }


        public async Task<ProductDTO> Save(ProductDTO product)
        {
            //MyTaskDTO result = null;
            //using (var client = new HttpClient())
            //{
            //    var content = new StringContent(JsonConvert.SerializeObject(myTask), Encoding.UTF8, "application/json");
            //    var callResult = await client.PostAsync($"https://internshipwebapp.azurewebsites.net/api/MyTasks/", content);
            //    var jsonResult = await callResult.Content.ReadAsStringAsync();
            //    result = JsonConvert.DeserializeObject<MyTaskDTO>(jsonResult);
            //}
            //return result;
            _products.Add(product);
            return product;
        }

        public async Task<ProductDTO> Update(int productId, ProductDTO product)
        {
            //UserDTO res = null;

            //using (var client = new HttpClient())
            //{

            //    var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            //    var callResult = await client.PutAsync($"https://internshipwebapp.azurewebsites.net/api/Users/{name}", content);
            //    if (callResult.IsSuccessStatusCode)
            //    {
            //        var jsonResult = await callResult.Content.ReadAsStringAsync();
            //        res = JsonConvert.DeserializeObject<UserDTO>(jsonResult);
            //    }
            //}

            //return res;

            var oldProduct = _products.FirstOrDefault(u => u.ProductId == productId);
            oldProduct.Name = product.Name;
            oldProduct.Quantity = product.Quantity;
            oldProduct.PostedDate = product.PostedDate;
            oldProduct.ExpirationDate = product.ExpirationDate;
            oldProduct.AsignedUser = product.AsignedUser;
            oldProduct.EncodedImage = product.EncodedImage;

            return oldProduct;
        }
        //FirebaseClient firebase = new FirebaseClient("https://xf-ell-d9f6b.firebaseio.com/");

        //public async Task<List<ProductDTO>> GetAll()
        //{
        //    List<ProductDTO> res = null;
        //    var call = (await firebase
        //      .Child("Countries")
        //      .OnceAsync<ProductDTO>()).Select(item => new ProductDTO
        //      {
        //          Name = item.Object.Name,
        //          CountryId = item.Object.CountryId
        //      }).ToList();



        //return res;
    }

    }

