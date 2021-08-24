using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MicroShop.Models;

namespace MicroShop.Data
{
    public class MicroShopContext : DbContext
    {
        public MicroShopContext (DbContextOptions<MicroShopContext> options)
            : base(options)
        {
        }

        public DbSet<MicroShop.Models.ShopItem> ShopItem { get; set; }
    }
}
