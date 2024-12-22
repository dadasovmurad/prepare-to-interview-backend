using PrepareToInterview.Application.DTOs.Answer;
using PrepareToInterview.Application.DTOs.Category;
using PrepareToInterview.Application.DTOs.Comment;
using PrepareToInterview.Application.DTOs.QuestionTranslations;
using PrepareToInterview.Application.DTOs.Tag;
using PrepareToInterview.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepareToInterview.Application.DTOs
{
    public class QuestionListDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        //public List<CategoryTranslationsListDto> CategoryTranslations { get; set; }
        //public CategoryDto Category { get; set; }
        public string Difficulty { get; set; }
        //public IList<AnswerListDto> Answers { get; set; }
        //public IList<CommentListDto> Comments { get; set; }
        //public IList<TagListDto> Tags { get; set; }
    }
}