using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepareToInterview.Domain.Entities.Common
{
    public class BaseEntities
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate => DateTime.Now;
    }
}
