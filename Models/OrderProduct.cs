using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace sistem_pemesanan_produk.Models
{
    [Table("OrderProducts")]
    public class OrderProduct
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
        
        public int Quantity { get; set; }
    }
}