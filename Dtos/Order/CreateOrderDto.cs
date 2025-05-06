using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sistem_pemesanan_produk.Dtos.Order
{
    public class CreateOrderDto
{
    public string? NamaPembeli { get; set; }

    public List<OrderItemDto>? Products { get; set; }
}

public class OrderItemDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}
}