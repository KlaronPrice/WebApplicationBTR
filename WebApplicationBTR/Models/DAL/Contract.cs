using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplicationBTR.Models;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationBTR.Models
{
    public partial class Contract
    {
        [Display(Name = "Скидка")]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string Discount { get; set; }

        public void InitializeDiscount()
        {
            if (ProductType.Name == "Заказ")
                Discount = (Price * 0.9).ToString();
            else
                Discount = "Без скидок";
        }
    }
}