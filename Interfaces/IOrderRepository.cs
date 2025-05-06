using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sistem_pemesanan_produk.Dtos.Order;
using sistem_pemesanan_produk.Models;

namespace sistem_pemesanan_produk.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> CreateOrderAsync(CreateOrderDto orderDto);
        Task<Order?> GetOrderByIdAsync(int id);
        Task<List<Order>> GetAllOrdersAsync();
        Task<Order?> DeleteOrderAsync(int id);
        Task<string> CreateOrderUsingStoredProcedureAsync(int kodeProduk, int qty, string pembeli);
    }
}