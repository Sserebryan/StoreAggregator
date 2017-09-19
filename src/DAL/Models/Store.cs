using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class Store: BaseEntity
    {
        public String Name { get; set; }

        public String ShortName { get; set; }
    }
}
