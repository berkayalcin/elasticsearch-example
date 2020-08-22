using System.Collections.Generic;
using System.Threading.Tasks;
using ElasticSearch_Example_Project.Domain.Catalog;
using ElasticSearch_Example_Project.Services.Dto;

namespace ElasticSearch_Example_Project.Services
{
    public interface IProductService
    {
        Task SaveProduct(Product product);
        Task<List<ProductOverviewModel>> Search(string productName, double price);

    }
}