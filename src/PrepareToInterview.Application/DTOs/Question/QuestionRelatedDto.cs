using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrepareToInterview.Application.DTOs.User;

namespace PrepareToInterview.Application.DTOs.Question
{
    public class QuestionRelatedDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public UserDetailsDto UserDetails { get; set; }
    }
}
