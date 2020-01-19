using Rent.DomainModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Rent.ViewModels.ProposalViewModels
{
    public class EditProposalDetailsViewModel
    {
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        public decimal ProposedPrice { get; set; }
        public string OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public virtual User Owner { get; set; }
        public string BuyerId { get; set; }
        [ForeignKey("BuyerId")]
        public virtual User Buyer { get; set; }
        //public ICollection<string>? BuyersComments { get; set; }
        //public ICollection<string>? OwnersComments { get; set; }
        public int ProposalStatusId { get; set; }
        [ForeignKey("ProposalStatusId")]
        public virtual ProposalStatus ProposalStatus { get; set; }
        public DateTime ProposalClosed { get; set; }
        public DateTime ProposedRentStartDate { get; set; }
        public DateTime ProposedRentEndDate { get; set; }
    }
}
