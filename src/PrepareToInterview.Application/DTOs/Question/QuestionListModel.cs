using PrepareToInterview.Application.DTOs.Question;

namespace PrepareToInterview.Application.DTOs.Question
{
    public class QuestionListModel
    {
        public IList<QuestionListDto> Items { get; set; } = new List<QuestionListDto>();
    }
}
