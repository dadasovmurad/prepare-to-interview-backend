using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepareToInterview.Application.DTOs.User
{
    public class UserCreatedDto
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string PlainPassKey { get; set; }
    }
}
