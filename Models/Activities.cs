using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FirstBeltExam.Models{
    public class Activity : BaseEntity{
        [Key]
        public int ActivityId { get; set; }
        public string ActivityName { get; set; }
        public string Time { get; set; }
        public DateTime ActivityDate { get; set; }
        public int ActivityDuration { get; set; }
        public string ActivityDescription { get; set; }
        public DateTime ActivityCreated_At { get; set; }
        public DateTime ActivityUpdated_At { get; set; }
        public string HDM { get ; set ;}
        public int UserId { get; set; }
        public User User { get; set; }
        public List<FunMaker> FunMaker { get; set; }
        public Activity(){
            FunMaker = new List<FunMaker>();
        }
    }
}