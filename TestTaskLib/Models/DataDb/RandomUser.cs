using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static TestTaskLib.DTOs.RandomUserDto;

namespace TestTaskLib.Models.DataDb
{
    public class RandomUser
    {
        [Key]
        public int Id { get; set; }
        public string Gender { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual Location Location { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Salt { get; set; }
        public string MD5 { get; set; }
        public string SHA1 { get; set; }
        public string SHA256 { get; set; }
        public DateTime DateOfBirthday { get; set; }
        public DateTime Registered { get; set; }
        public string Phone { get; set; }
        public string Cell { get; set; }
        public virtual Picture Picture { get; set; }
        public virtual List<File> Files { get; set; } = new List<File>();
        public string Nat { get; set; }
    }
}
