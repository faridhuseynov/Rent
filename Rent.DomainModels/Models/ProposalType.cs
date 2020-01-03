using System;
using System.Collections.Generic;
using System.Text;

namespace Rent.DomainModels.Models
{
    public class ProposalType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public virtual ICollection<Proposal> Proposals { get; set; }
    }
}
