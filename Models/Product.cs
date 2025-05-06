using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace sistem_pemesanan_produk.Models
{
    [Table("Products")]
    public class Product
    {
        public int Id { get; set; }
        public string NamaProduk { get; set; } = string.Empty;
        public string Deskripsi { get; set; } = string.Empty;
        public decimal Harga { get; set; }  

        public List<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
    }
}