using System;

namespace WebAPI.Models.Responses
{
    public class TokenResponse
    {
        public string Token { get; set; }
        public string UserName { get; set; }
        public DateTime Expires { get; set; }
    }
}
