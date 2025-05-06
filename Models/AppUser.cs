using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace sistem_pemesanan_produk.Models
{
    public class AppUser : IdentityUser
    {
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}