using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepareToInterview.Application.DTOs.Category
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //public CategoryDto Parent { get; set; } 
        //public List<CategoryDto> Children { get; set; }
    }
}
