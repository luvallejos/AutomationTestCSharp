using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UITestFramework.Dto;

namespace UITestFramework.Utilities
{
    public class APIClientHelper
    {
        #region Constants
        private readonly HttpClient _client;
        #endregion

        #region Constructors
        public APIClientHelper(string baseUrl)
        {
            _client = new HttpClient { BaseAddress = new Uri(baseUrl) };
        }
        #endregion

        #region Methods
        public async Task<List<Product>> GetAllProductsList()
        {
            var response = await _client.GetAsync($"/api/productsList");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var apiResponse = JsonConvert.DeserializeObject<ApiProductsResponse>(json);

            var products = apiResponse.Products.Select(p => new Product
            {
                Name = p.Name,
                Price = ParsePrice(p.Price),
                Brand = ParseBrand(p.Brand),
                Category = new CategoryInfo
                {
                    UserType = ParseUserType(p.Category?.UserType?.UserType),
                    Category = ParseCategory(p.Category?.Category)
                },
                Availability = p.Availability,
                Condition = p.Condition
            }).ToList();

            return products;
        }

        public async Task<bool> DoesUserAccountExists(string email)
        {
            var response = await _client.GetAsync($"/api/getUserDetailByEmail?email={email}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return response.StatusCode == HttpStatusCode.OK &&
                    content.Contains("\"responseCode\": 200");
        }

        private int ParsePrice(string priceText)
        {
            if (string.IsNullOrWhiteSpace(priceText))
                return 0;

            var digits = Regex.Replace(priceText, @"[^\d]", "");
            return int.TryParse(digits, out var value) ? value : 0;
        }

        private ProductBrand? ParseBrand(string brandText)
        {
            if (string.IsNullOrWhiteSpace(brandText))
                return null;

            var normalized = brandText.Replace("&", "").UnescapeTextAndRemoveSpaces();

            return Enum.TryParse<ProductBrand>(normalized, true, out var brand)
                ? (ProductBrand?)brand
                : null;
        }

        private ProductUserType? ParseUserType(string userTypeText)
        {
            if (string.IsNullOrWhiteSpace(userTypeText))
                return null;

            return Enum.TryParse<ProductUserType>(userTypeText, true, out var ut)
                ? (ProductUserType?)ut
                : null;
        }

        private ProductCategory? ParseCategory(string categoryText)
        {
            if (string.IsNullOrWhiteSpace(categoryText))
                return null;

            var normalized = categoryText.Replace("&", "").UnescapeTextAndRemoveSpaces();

            return Enum.TryParse<ProductCategory>(normalized, true, out var cat)
                ? (ProductCategory?)cat
                : null;
        }

        #endregion
    }
}
