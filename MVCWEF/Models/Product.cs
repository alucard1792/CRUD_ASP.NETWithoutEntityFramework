using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//DisplayName import
using System.ComponentModel;

namespace MVCWEF.Models
{
    public class Product
    {
        public int id { get; set; }
        [DisplayName("product name")]
        public string name { get; set; }
        public decimal price { get; set; }
        public int count { get; set; }

    }
}