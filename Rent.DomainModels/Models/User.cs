using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rent.DomainModels.Models
{
    public class User : IdentityUser
    {
        //public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        //public string Username { get; set; }
        public int WishProductsCount { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Proposal> Proposals { get; set; }
        public virtual ICollection<WishListProduct> WishList { get; set; }
        public string MeetingLocation { get; set; }
        public virtual ICollection<ProfileImage> ProfileImages { get; set; }
        public string MainProfilePicture { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Gender { get; set; }
    }
}
