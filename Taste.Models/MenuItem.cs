using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Taste.Models
{
    public class MenuItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Price should be greater than 1$." )]
        public double Price { get; set; }
        [Display(Name="Category Type")]

        [ForeignKey(name:"CategoryId")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual FoodType FoodType { get; set; }
        [ForeignKey(name: "FoodTypeId")]
        [Display(Name = "Food Type")]
        public int FoodTypeId { get; set; }
    }
}
