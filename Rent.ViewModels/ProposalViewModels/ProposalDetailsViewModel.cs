﻿using Rent.DomainModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Rent.ViewModels.ProposalViewModels
{
    public class ProposalDetailsViewModel
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
        public int ProposalTypeId { get; set; }
        public virtual ProposalType ProposalType { get; set; }
        public bool ProposalStatus { get; set; }
    }
}
