using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Taste.Models
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            Count = 1;
        }
        public int Id { get; set; }
        public int MenuItemId { get; set; }
        public string ApplicationUserId { get; set; }
        [Range(1,100, ErrorMessage ="Please select a count between 1 and 100")]
        public int Count { get; set; }
        [ForeignKey(name: "ApplicationUserId")]
        [NotMapped]
        public virtual ApplicationUser ApplicationUser { get; set; }
        [ForeignKey(name: "MenuItemId")]
        [NotMapped]
        public virtual MenuItem MenuItem { get; set; }
    }
}
