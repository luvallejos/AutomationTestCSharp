
namespace UITestFramework.Dto
{
    public class CartTableRow
    {
        #region Properties
        public string ProductIdIndex { get; set; }
        public string ProductName { get; set; }
        public string ProductCategoryUserType { get; set; }
        public string ProductCategoryCategory { get; set; }
        public int ProductPrice { get; set; }
        public int ProductQuantity { get; set; }
        public int ProductTotalPrice { get; set; }

        #endregion

        #region constructors
        public CartTableRow() { }
        #endregion
    }
}
