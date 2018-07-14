using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationBTR.Models
{
    public class ProductType
    {
        public int ProductTypeId { get; set; }

        [Display(Name = "Тип продукта")]
        public string Name { get; set; }
    }
}