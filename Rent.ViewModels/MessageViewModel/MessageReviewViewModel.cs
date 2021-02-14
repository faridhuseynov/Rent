using Rent.DomainModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Rent.ViewModels.MessageViewModel
{
    public class MessageReviewViewModel
    {
        public int Id { get; set; }
        public string SenderId { get; set; }
        public string SenderUsername { get; set; }
        public string SenderMainPhotoUrl { get; set; }
        public string RecipientId { get; set; }
        [ForeignKey("RecipientId")]
        public string RecipientUsername { get; set; }
        public string RecipientMainPhotoUrl { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; }
        public DateTime MessageSent { get; set; }
    }
}
