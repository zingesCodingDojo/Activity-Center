using System;
using System.ComponentModel.DataAnnotations;

namespace FirstBeltExam.Models{
    public class RegisterViewModel : BaseEntity{
        [Required]
        [MinLength(3, ErrorMessage = "We need your First Name to be at least 3 characters long.")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Sorry, First Name only accepts letters.")]
        public string FirstName { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "We need your Last Name to be at least 3 characters long.")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Sorry, Last Name only accepts letters.")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "We need your Password to be at least 8 characters long.")]
        [RegularExpression(@"(?=.{8,})(?=.*[a-zA-Z])(?=.*[!@#$%^&+=])(?=.*\d).*", ErrorMessage = "Password needs a special character and number.")]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

    }
}