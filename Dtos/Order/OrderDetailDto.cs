using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sistem_pemesanan_produk.Dtos.Order
{
    public class OrderDetailDto
{
    public int Id { get; set; }
    public string? NamaPembeli { get; set; }
    public DateTime CreatedAt { get; set; }

    public List<OrderProductDetailDto>? Products { get; set; }

    public decimal TotalHarga => Products?.Sum(p => p.Harga * p.Quantity) ?? 0;
}

public class OrderProductDetailDto
{
    public int ProductId { get; set; }
    public string? NamaProduk { get; set; }
    public decimal Harga { get; set; }
    public int Quantity { get; set; }
}
}