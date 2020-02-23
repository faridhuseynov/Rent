using System;
using System.Collections.Generic;
using System.Text;

namespace Rent.DomainModels.Models
{
    public class ProfileImage
    {
        public int Id { get; set; }
        public string PhotoUrl { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
