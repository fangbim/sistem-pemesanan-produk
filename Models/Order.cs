using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace sistem_pemesanan_produk.Models
{
    [Table("Orders")]
    public class Order
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; } = DateTime.Now;

        public List<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
    }
}