using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationBTR.Models
{
    public partial class Contract
    {
        
        public int ContractId { get; set; }

        
        [RegularExpression(@"^\d+$", ErrorMessage = "Некорректное значение")]
        [Display(Name = "Номер контракта")]
        [Required]
        public int ContractNumber { get; set; }

        [Range(0, 100000)]
        [RegularExpression(@"^\d+$", ErrorMessage = "Некорректное значение")]
        [Display(Name = "Цена")]
        [Required]
        public int Price { get; set; }

        [Required]
        public Person Person { get; set; }

        [Display(Name = "Тип контракта")]
        [Required]
        public ProductType ProductType { get; set; }
     

        
    }
}