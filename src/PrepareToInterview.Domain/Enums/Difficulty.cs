using System.ComponentModel;

namespace PrepareToInterview.Domain.Enums
{
    public enum Difficulty
    {
        [Description("Asan")]
        Easy,
        [Description("Orta")]
        Medium,
        [Description("Çətin")]
        Hard
    }
    public enum ContributionStatus
    {
        [Description("Gözləmədə")]
        Pending,
        [Description("Rədd olundu")]
        Rejected,
        [Description("Qəbul olundu")]
        Accepted
    }
}