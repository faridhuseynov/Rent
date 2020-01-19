using System;
using System.Collections.Generic;
using System.Text;

namespace Rent.DomainModels.Models
{
    public class ProposalStatus
    {
        public int Id { get; set; }
        public string StatusName { get; set; }
        public virtual ICollection<Proposal> Proposals { get; set; }
    }
}
