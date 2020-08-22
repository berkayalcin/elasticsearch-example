using System.Collections;
using System.Collections.Generic;
using Nest;

namespace ElasticSearch_Example_Project.Domain.Catalog
{
    [ElasticsearchType(RelationName = "category", IdProperty = "Id")]
    public class Category
    {

        [Text(Name = "title")]
        public string Title { get; set; }
        [Text(Name = "description")]
        public string Description { get; set; }
    }
}