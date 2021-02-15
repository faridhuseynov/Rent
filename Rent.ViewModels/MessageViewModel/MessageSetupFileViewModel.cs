using System;
using System.Collections.Generic;
using System.Text;

namespace Rent.ViewModels.MessageViewModel
{
    public class MessageSetupFileViewModel
    {
        public string CurrentUser { get; set; }
        public IEnumerable<MessageReviewViewModel> SortedMessages { get; set; }
    }
}
