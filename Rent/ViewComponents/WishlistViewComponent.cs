using Microsoft.AspNetCore.Mvc;
using Rent.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rent.ViewComponents
{
    public class WishlistViewComponent:ViewComponent
    {
        private readonly IWishListProdsRepository wr;

        public WishlistViewComponent(IWishListProdsRepository wr)
        {
            this.wr = wr;
        }

        public async Task<IViewComponentResult> InvokeAsync(string userName)
        {
            var wishList = await wr.GetWishListProducts(userName);
            return View(wishList);
        }
    }
}
