using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveUpApp.Models
{
    public class Product
    {
        public string Description { get; set; }
        public double Price { get; set; }
        public DateTime DateSaved { get; set; }
    }
}