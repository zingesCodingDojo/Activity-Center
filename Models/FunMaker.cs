using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstBeltExam.Models{
    public class FunMaker : BaseEntity{
        [Key]
        public int FunMakerId { get; set; }
        public string FunMakerAction { get; set; }
        [ForeignKey("ActivityId")]
        public int ActivityId { get; set; }
        public Activity Activity { get; set; }
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime FunMakerCreated_At { get; set; }
        public DateTime FunMakerUpdated_At { get; set; }
        }
}