using Newtonsoft.Json;
using System.Collections.Generic;

namespace UITestFramework.Dto
{
    public class ApiProductsResponse
    {
        [JsonProperty("responseCode")]
        public string ResponseCode { get; set; }

        [JsonProperty("products")]
        public List<ApiProduct> Products { get; set; }
    }

    public class ApiProduct
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("brand")]
        public string Brand { get; set; }

        [JsonProperty("category")]
        public ApiCategory Category { get; set; }

        [JsonProperty("availability")]
        public string Availability { get; set; }

        [JsonProperty("condition")]
        public string Condition { get; set; }
    }

    public class ApiCategory
    {
        [JsonProperty("usertype")]
        public ApiUserType UserType { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }
    }

    public class ApiUserType
    {
        [JsonProperty("usertype")]
        public string UserType { get; set; }
    }
}
