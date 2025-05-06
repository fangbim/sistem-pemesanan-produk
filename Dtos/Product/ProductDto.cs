using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sistem_pemesanan_produk.Dtos.Product
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string NamaProduk { get; set; } = string.Empty;
        public string Deskripsi { get; set; } = string.Empty;
        public decimal Harga { get; set; }  
    }
}