using Rent.DomainModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Rent.ViewModels.ProposalViewModels
{
    public class NewProposalViewModel
    {
        public int ProductId { get; set; }
        public decimal ProposedPrice { get; set; }
        public string OwnerId { get; set; }
        public string BuyerId { get; set; }
        //public ICollection<string>? BuyersComments { get; set; }
        //public ICollection<string>? OwnersComments { get; set; }
        public int ProposalTypeId { get; set; }
        public virtual ProposalType ProposalType { get; set; }

        public bool ProposalStatus { get; set; }
    }
}
