using System;
using ElasticSearch_Example_Project.Domain.Catalog;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace ElasticSearch_Example_Project.Extensions
{
    public static class ElasticSearchExtensions
    {
        /// <summary>
        /// Add ElasticSearch
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        /// <param name="configuration">Configuration</param>
        public static void AddElasticSearch(this IServiceCollection services, IConfiguration configuration)
        {
            // Elastic Search Url
            var url = configuration["ElasticSearch:url"];
            // Default Index Name
            var defaultIndex = configuration["ElasticSearch:index"];

            var settings = new ConnectionSettings(new Uri(url))
                .DefaultIndex(defaultIndex);

            var client = new ElasticClient(settings);
            services.AddSingleton<IElasticClient>(client);
        }
    }
}