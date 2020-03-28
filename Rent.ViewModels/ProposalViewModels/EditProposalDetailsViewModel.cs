using Rent.DomainModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Rent.ViewModels.ProposalViewModels
{
    public class EditProposalDetailsViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        public int ProposalTypeId { get; set; }
        public virtual ProposalType ProposalType { get; set; }
        public decimal ProposedPrice { get; set; }
        public DateTime ProposedRentStartDate { get; set; }
        public DateTime ProposedRentEndDate { get; set; }
        public string ProposalMessage { get; set; }

    }
}
