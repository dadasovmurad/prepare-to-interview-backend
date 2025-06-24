using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepareToInterview.Application.DTOs.User
{
    public class UserDetailsDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string? PersonalUrl { get; set; }
        public string? ImageUrl { get; set; }
    }
}
