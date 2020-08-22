using System.Collections.Generic;
using System.Threading.Tasks;
using ElasticSearch_Example_Project.Domain.Catalog;
using ElasticSearch_Example_Project.Services.Dto;

namespace ElasticSearch_Example_Project.Services
{
    /// <summary>
    /// Product Elastic Service
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Save Product Document
        /// </summary>
        /// <param name="product">Product Document</param>
        /// <returns></returns>
        Task SaveProduct(Product product);

        /// <summary>
        /// Search Product Documents
        /// </summary>
        /// <param name="searchQuery">Product Name</param>
        /// <param name="price">Product Price</param>
        /// <returns></returns>
        Task<List<ProductOverviewModel>> Search(string searchQuery, double? price = null);

    }
}