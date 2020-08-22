using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElasticSearch_Example_Project.Domain.Catalog;
using ElasticSearch_Example_Project.Services.Dto;
using Nest;

namespace ElasticSearch_Example_Project.Services
{
    /// <inheritdoc cref="IProductService"/>
    public class ProductService : IProductService
    {
        private readonly IElasticClient _elasticClient;
        private static readonly List<Product> _products = new List<Product>()
        {
            new Product(){Id = 1,Price = 10,Title = "Iphone 5",Description = "Iphone 5 Mobile Phone"},
            new Product(){Id = 2,Price = 15,Title = "Iphone 6",Description = "Iphone 6 Mobile Phone"},
            new Product(){Id = 3,Price = 100,Title = "Iphone 7",Description = "Iphone 7 Mobile Phone"},
            new Product(){Id = 4,Price = 150,Title = "Iphone 4",Description = "Iphone 8X Mobile Phone"},
            new Product(){Id = 5,Price = 210,Title = "Iphone 8 X",Description = "Iphone 10X Mobile Phone"},
            new Product(){Id = 6,Price = 510,Title = "Iphone 10 X",Description = "Iphone 10X Mobile Phone"},
            new Product(){Id = 7,Price = 110,Title = "Iphone 10 X-Max",Description = "Iphone 10X Max Mobile Phone"},
            new Product(){Id = 8,Price = 210,Title = "Iphone 11 XR",Description = "Iphone 11 XR Mobile Phone"},
            new Product(){Id = 9,Price = 120,Title = "Iphone 5S",Description = "Iphone 5S Mobile Phone"},
            new Product(){Id = 10,Price = 102,Title = "Iphone 5C",Description = "Iphone 5C Mobile Phone"},
            new Product(){Id = 11,Price = 105,Title = "Iphone 6S",Description = "Iphone 6S Mobile Phone"},
            new Product(){Id = 12,Price = 610,Title = "Iphone 6+",Description = "Iphone 6+ Mobile Phone"},
            new Product(){Id = 13,Price = 1077,Title = "Iphone 6 Plus",Description = "Iphone 6+ Mobile Phone"},
            new Product(){Id = 14,Price = 1110,Title = "Iphone 7",Description = "Iphone 7 Mobile Phone"},
            new Product(){Id = 15,Price = 1120,Title = "Iphone 7S",Description = "Iphone 7S Mobile Phone"},
            new Product(){Id = 16,Price = 16610,Title = "Iphone 7+",Description = "Iphone 7+ Mobile Phone"},
            new Product(){Id = 17,Price = 1110,Title = "Iphone 12",Description = "Iphone 12 Mobile Phone"},
        };

        /// <summary>
        /// Product Elastic Service
        /// </summary>
        /// <param name="elasticClient">Elastic Client</param>
        public ProductService(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }


        public async Task<List<ProductOverviewModel>> Search(string searchQuery, double? price = null)
        {


            var searchResponse = _elasticClient.Search<Product>(search =>
                  search
                      .Query(query =>
                      query.QueryString(c =>
                          c
                              .Boost(1.1)
                              .Fields(x => x.Field(fi => fi.Description))
                              .Query(searchQuery)
                              .Analyzer("standard")
                              .DefaultOperator(Operator.Or)
                              .Lenient()
                              .AnalyzeWildcard()
                              .MinimumShouldMatch("40%")
                              .FuzzyPrefixLength(0)
                              .FuzzyMaxExpansions(50)
                              .FuzzyTranspositions()
                              .AutoGenerateSynonymsPhraseQuery(false)
                      ) ||
                      query.QueryString(c =>
                          c
                              .Boost(1.1)
                              .Fields(x => x.Field(fi => fi.Title))
                              .Query(searchQuery)
                              .Analyzer("standard")
                              .DefaultOperator(Operator.Or)
                              .Lenient()
                              .AnalyzeWildcard()
                              .MinimumShouldMatch("40%")
                              .FuzzyPrefixLength(0)
                              .FuzzyMaxExpansions(50)
                              .FuzzyTranspositions()
                              .AutoGenerateSynonymsPhraseQuery(false)
                      ) ||
                      query.Range(r =>
                          r.Field(fi => fi.Price).GreaterThanOrEquals(price)) ||
                      query.Range(r =>
                          r.Field(fi => fi.Price).LessThanOrEquals(price))
                    )
            );
            var products = searchResponse.Documents.ToList();
            return products.Select(t => new ProductOverviewModel()
            {
                Description = t.Description,
                Price = t.Price,
                Id = t.Id,
                Title = t.Title
            }).ToList();
        }

        public async Task SaveProduct(Product product)
        {
            var products = _products;
            products.Add(product);

            //await _elasticClient.Indices.DeleteAsync("products");

            if (!(await _elasticClient.Indices.ExistsAsync("products")).Exists)
            {

                var createIndexResponse = await _elasticClient.Indices.CreateAsync("products", i => i.Map<Product>(m => m.AutoMap()));
            }

            var indexManyAsync = await _elasticClient.IndexManyAsync(products, "products");


        }
    }
}