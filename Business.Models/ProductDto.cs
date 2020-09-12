﻿using System.Collections.Generic;
using System.ComponentModel;

namespace Business.Models
{
    public class ProductDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public decimal Price { get; set; }
        
        public string ImageUrl { get; set; }
        
        public string Description { get; set; }
        
        public CategoryDto Category { get; set; }
    }
}