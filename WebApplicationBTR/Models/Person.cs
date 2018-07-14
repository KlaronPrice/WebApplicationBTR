using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationBTR.Models
{
    public class Person
    {
        public int PersonId { get; set; }

        
        [Required]
        public Organization Organization { get; set; }

        [RegularExpression(@"^\D+$", ErrorMessage = "Некорректное значение")]
        [Display(Name = "ФИО")]
        [Required]
        public string Name { get; set; }
        

        public override string ToString()
        {
            return Name;
        }
    }
}