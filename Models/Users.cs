using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FirstBeltExam.Models{
    public abstract class BaseEntity{}
    public class User : BaseEntity{
        [Key]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime UserCreated_At { get; set; }
        public DateTime UserUpdated_At { get; set; }
        public List<Activity> Activity { get; set; }
        public List<FunMaker> FunMaker { get; set; }
        public User(){
            Activity = new List<Activity>();
            FunMaker = new List<FunMaker>();
        }
    }
}