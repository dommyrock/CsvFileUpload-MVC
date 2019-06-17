using Newtonsoft.Json;
using System.Linq;

namespace CsvFileReaderApp.OData
{
    public class PagedResult
    {
        /// <summary>
        /// Number of records skipped.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int? Skip { get; set; }

        /// <summary>
        /// Number of records returned.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int? Top { get; set; }

        /// <summary>
        /// The total number of records available.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int? Count { get; set; }

        /// <summary>
        /// The records this page represents.
        /// </summary>
        public IQueryable Data { get; set; }
    }
}