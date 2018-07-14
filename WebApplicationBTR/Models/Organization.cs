using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationBTR.Models
{
    public class Organization
    {
        public int OrganizationId { get; set; }

        [RegularExpression(@"^\D+$", ErrorMessage = "Некорректное значение")]
        [Display(Name = "Организация")]
        [Required]
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}