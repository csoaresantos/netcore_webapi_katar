using System;
using System.ComponentModel.DataAnnotations;

namespace interview_katar
{
    public class ShoppingItem
    {
        public Int32 Id { get; set; }
        [Required]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Manufacturer { get; set; }

    }
}
