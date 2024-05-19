using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Identity_AND_Security.ViewModel
{
    public class Profile_ViewModel
    {
        [Display(Name = "Full Name")]
        public string? FullName { get; set; }


        public IFormFile? Picture { get; set; }
    }
}
