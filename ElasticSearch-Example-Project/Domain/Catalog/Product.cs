using Nest;

namespace ElasticSearch_Example_Project.Domain.Catalog
{
    /// <summary>
    /// Product Document
    /// </summary>
    [ElasticsearchType(RelationName = "product", IdProperty = "id")]
    public class Product
    {
        /// <summary>
        /// Product Id
        /// </summary>
        [Number(Name = "id")]
        public int Id { get; set; }

        /// <summary>
        /// Product Title
        /// </summary>
        [Text(Name = "title")]
        public string Title { get; set; }

        /// <summary>
        /// Product Price
        /// </summary>
        [Number(Name = "price")]
        public double Price { get; set; }

        /// <summary>
        /// Product Description
        /// </summary>
        [Text(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Product Category
        /// </summary>
        [Object(Name = "category", Ignore = true)]
        public Category Category { get; set; }
    }
}