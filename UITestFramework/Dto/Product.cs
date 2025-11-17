
namespace UITestFramework.Dto
{
    public class Product
    {
        public string Name { get; set; }
        public CategoryInfo Category { get; set; }
        public ProductBrand? Brand { get; set; }
        public int Price { get; set; }
        public string Availability { get; set; }
        public string Condition { get; set; }
    }
}
