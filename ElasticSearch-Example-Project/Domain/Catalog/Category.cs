using System.Collections;
using System.Collections.Generic;
using Nest;

namespace ElasticSearch_Example_Project.Domain.Catalog
{
    /// <summary>
    /// Category Document
    /// </summary>
    [ElasticsearchType(RelationName = "category", IdProperty = "Id")]
    public class Category
    {

        /// <summary>
        /// Category Title
        /// </summary>
        [Text(Name = "title")]
        public string Title { get; set; }
        /// <summary>
        /// Category Description
        /// </summary>
        [Text(Name = "description")]
        public string Description { get; set; }
    }
}