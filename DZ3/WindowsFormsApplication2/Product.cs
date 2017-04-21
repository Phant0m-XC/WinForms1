using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication2
{
    public class Product
    {
        public string Name { get; set; }
        public string Charactiristics { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public Product()
        {
            Name = null;
            Charactiristics = null;
            Description = null;
            Price = 0;
        }
    }
}
