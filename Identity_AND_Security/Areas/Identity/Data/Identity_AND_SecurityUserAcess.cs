using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Identity_AND_Security.Areas.Identity.Data;

// Add profile data for application users by adding properties to the Identity_AND_SecurityUserAcess class


public class Identity_AND_SecurityUserAcess : IdentityUser
{
    
    [Display(Name ="Full Name")]
    public string? FullName { get; set; }

    [ValidateNever]
    public string? PicPath { get; set; }

    [NotMapped]
    public IFormFile Picture {  get; set; }

}

