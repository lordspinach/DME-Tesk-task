using System;
using TestTaskLib.Models.DataDb;

namespace WebAPI.Models.Responses
{
    public class UserResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirtday { get; set; }
        //public string? Picture { get; set; }

        public UserResponse(RandomUser user)
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
            DateOfBirtday = user.DateOfBirthday;
            //Picture = user.Picture.Large;
        }
    }
}
