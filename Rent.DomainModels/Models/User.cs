using System;
using System.Collections.Generic;
using System.Text;

namespace Rent.DomainModels.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Proposal> Proposals { get; set; }

    }
}
