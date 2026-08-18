using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeYourOwnPizza.Domain.Entities
{
    public class Pizza
    {
        public Guid Id { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
    }
}
