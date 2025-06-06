using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DotNetSpaAuth.Models
{

    public class User : IdentityUser
    {
        [Required]
        public string Firstname { get; init; } = string.Empty;

        [Required]
        public string Lastname { get; init; } = string.Empty;
    }
}
