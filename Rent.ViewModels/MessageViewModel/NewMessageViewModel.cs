using System;
using System.Collections.Generic;
using System.Text;

namespace Rent.ViewModels.MessageViewModel
{
    public class NewMessageViewModel
    {
        public int Id { get; set; }
        public string SenderId { get; set; }
        public string RecipientId { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; } = false;
        public DateTime MessageSent { get; set; }
    }
}
