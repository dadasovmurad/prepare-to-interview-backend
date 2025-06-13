using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using PrepareToInterview.Domain.Entities.Common;
using PrepareToInterview.Domain.Enums;

namespace PrepareToInterview.Domain.Entities
{
    public class Contribution : BaseEntity
    {
        public int UserId { get; set; }
        public string QuestionTitle { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public Difficulty Difficulty { get; set; }
        public JsonArray? Tags { get; set; }
        public string Experience { get; set; }
        public string Answer { get; set; }
        public User User { get; set; }
    }
}