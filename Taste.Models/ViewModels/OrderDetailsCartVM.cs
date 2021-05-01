using System.Collections.Generic;

namespace Taste.Models.ViewModels
{
    public class OrderDetailsCartVM
    {
        public OrderDetailsCartVM()
        {
            ListOfShoppingCart = new();
        }
        public List<ShoppingCart> ListOfShoppingCart { get; set; }
        public OrderHeader OrderHeader { get; set; }
    }
}
