using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginReg.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Display(Name="First Name")]
        [Required (ErrorMessage="What's your name again?")]
        [MinLength(2)]
        public string FirstName { get; set; }

        [Display(Name="Last Name")]
        [Required (ErrorMessage="We need your last name too.")]
        [MinLength(2)]
        public string LastName { get; set; }

        [Display(Name="Email")]
        [Required (ErrorMessage="Email please! Otherwise we won't know where to spam you.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name="Password")]
        [Required (ErrorMessage="I know your password but put it again anyways.")]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name="Confirm Password")]
        [Required]
        [NotMapped]
        [Compare("Password", ErrorMessage = "Confirm your password - it's 'password'.")]
        [DataType(DataType.Password)]
        public string ConfirmPW { get; set; }
    }
}