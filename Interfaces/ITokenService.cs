using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sistem_pemesanan_produk.Models;

namespace sistem_pemesanan_produk.Interfaces
{
    public interface ITokenService
    {
        public string CreateToken(AppUser user);
    }
}