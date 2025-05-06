using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using sistem_pemesanan_produk.Data;
using sistem_pemesanan_produk.Dtos.Order;
using sistem_pemesanan_produk.Interfaces;
using sistem_pemesanan_produk.Models;

namespace sistem_pemesanan_produk.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDBContext _context;
        public OrderRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Order> CreateOrderAsync(CreateOrderDto orderDto)
        {
            var order = new Order
            {
                Username = orderDto.NamaPembeli,
                OrderDate = DateTime.Now,
                OrderProducts = new List<OrderProduct>()
            };

            foreach (var item in orderDto.Products)
            {
                var product = await _context.Products.FindAsync(item.ProductId);
                if (product == null ) throw new Exception("Product not found");

                order.OrderProducts.Add(new OrderProduct
                {
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                }); 
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return order;
        }

        public async Task<string> CreateOrderUsingStoredProcedureAsync(int kodeProduk, int qty, string pembeli)
        {
            var result = await _context.Database
                .SqlQueryRaw<string>("EXEC sp_CreateOrder @p0, @p1, @p2", kodeProduk, qty, pembeli)
                .ToListAsync();

            return result.FirstOrDefault() ?? "Gagal";
        }

        public async Task<Order?> DeleteOrderAsync(int id)
        {
            var order = _context.Orders.Find(id);
            if (order == null) return null;

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return order;
        }

        public Task<List<Order>> GetAllOrdersAsync()
        {
            return _context.Orders
                .Include( op => op.OrderProducts)
                .ThenInclude(p => p.Product)
                .ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _context.Orders
                .Include(op => op.OrderProducts)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(o => o.Id == id);
        }
    }
}