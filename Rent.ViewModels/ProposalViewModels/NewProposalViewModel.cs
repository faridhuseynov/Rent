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
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        public decimal ProposedPrice { get; set; }
        public decimal ProposedTotalPrice { get; set; }
        public string OwnerId { get; set; }
        public string BuyerId { get; set; }
        //public ICollection<string>? BuyersComments { get; set; }
        //public ICollection<string>? OwnersComments { get; set; }
        public int ProposalTypeId { get; set; }
        public virtual ProposalType ProposalType { get; set; }

        public int ProposalStatusId { get; set; }
        [ForeignKey("ProposalStatusId")]
        public virtual ProposalStatus ProposalStatus { get; set; }
        public DateTime ProposalAdded { get; set; }
        public DateTime ProposedRentStartDate { get; set; }
        public DateTime ProposedRentEndDate { get; set; }
        public string ProposalMessage { get; set; }
        public int ProposedAmount { get; set; }
    }
}
