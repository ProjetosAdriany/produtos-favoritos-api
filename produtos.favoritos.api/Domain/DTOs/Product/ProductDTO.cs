using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Product
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public float Price { get; set; }
    }
}
