﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Taste.Models
{
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        [ForeignKey(name:"OrderId")]
        public virtual OrderHeader OrderHeader { get; set; }

        public int MenuItemId { get; set; }
        [ForeignKey(name: "MenuItemId")]
        public virtual MenuItem MenuItem { get; set; }
        public int Count { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }


    }
}
