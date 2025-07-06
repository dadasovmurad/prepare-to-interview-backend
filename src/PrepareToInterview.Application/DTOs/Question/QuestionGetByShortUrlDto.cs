using PrepareToInterview.Application.DTOs.Tag;
using PrepareToInterview.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepareToInterview.Application.DTOs.Question
{
    public class QuestionGetByShortUrlDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Difficulty { get; set; }
        public string ShortUrl { get; set; }
        public IList<TagListDto> Tags { get; set; }
        public UserDetailsDto UserDetails { get; set; }
    }
}
