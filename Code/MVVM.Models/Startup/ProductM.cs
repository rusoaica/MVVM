/// Written by: Yulia Danilova
/// Creation Date: 25th of January, 2021
/// Purpose: Model for products
#region ========================================================================= USING =====================================================================================
using MVVM.Models.Common.MVVM;
#endregion

namespace MVVM.Models.Startup
{
    public class ProductM : LightBaseModel
    {
        #region ================================================================ PROPERTIES =================================================================================
        public string ArticleNumber { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        #endregion

        #region ============================================================ BINDING PROPERTIES =============================================================================
        private int quantity;
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; Notify(nameof(Quantity)); }
        }

        private decimal totalPrice;
        public decimal TotalPrice
        {
            get { return totalPrice; }
            set { totalPrice = value; Notify(nameof(TotalPrice)); }
        }
        #endregion

        #region ================================================================= METHODS ===================================================================================
        /// <summary>
        /// Customized ToString() method
        /// </summary>
        /// <returns>Custom string value showing relevant data for current class</returns>
        public override string ToString()
        {
            return Name + " :: " + Price;
        }
        #endregion
    }
}
