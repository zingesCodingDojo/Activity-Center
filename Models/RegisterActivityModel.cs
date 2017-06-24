using System;
using System.ComponentModel.DataAnnotations;

namespace FirstBeltExam.Models{
    public class RegisterActivityModel : BaseEntity{
        [Required]
        [MinLength(2, ErrorMessage = "Activity Title needs to be longer than 2 characters.")]
        public string ActivityName { get; set; }
        [Required]
        public string Time { get; set; }
        [Required(ErrorMessage = "Activity Date is required.")]
        public DateTime ActivityDate { get; set; }
        [Required(ErrorMessage = "Activity Duration is required.")]
        public int ActivityDuration { get; set; }
        [Required]
        [MinLength(10, ErrorMessage = "Activity Description needs to be longer than 10 characters.")]
        public string ActivityDescription { get; set; }
        public string HDM { get ; set ;}

    }
}