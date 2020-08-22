using Nest;

namespace ElasticSearch_Example_Project.Domain.Catalog
{
    [ElasticsearchType(RelationName = "product", IdProperty = "id")]
    public class Product
    {
        [Number(Name = "id")]
        public int Id { get; set; }

        [Text(Name = "title")]
        public string Title { get; set; }

        [Number(Name = "price")]
        public double Price { get; set; }

        [Text(Name = "description")]
        public string Description { get; set; }

        [Object(Name = "category", Ignore = true)]
        public Category Category { get; set; }
    }
}