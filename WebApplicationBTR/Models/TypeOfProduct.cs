using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace WebApplicationBTR.Models
{
    public class TypeOfProduct
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int TypeOfProductID;

        public string Name;

        public bool HaveDiscount()
        {
            if (Name == "Заказ")
                return true;
            return false;
        }
    }
}