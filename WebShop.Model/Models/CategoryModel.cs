﻿using System.ComponentModel.DataAnnotations;

namespace WebShop.Models
{
    public class CategoryModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int DisplayOrder { get; set; }

        public DateTime CreatedDateTime { get; set; } 
    }
}
