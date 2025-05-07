using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using sistem_pemesanan_produk.Dtos.Order;
using sistem_pemesanan_produk.Interfaces;
using sistem_pemesanan_produk.Models;

namespace sistem_pemesanan_produk.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepo;
        public OrderController(IOrderRepository orderRepo)
        {
            _orderRepo = orderRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderRepo.GetAllOrdersAsync();
            if (orders == null || !orders.Any()) return NotFound();

            var result = new List<OrderDetailDto>();
            foreach (var order in orders)
            {
                var orderDetail = new OrderDetailDto
                {
                    Id = order.Id,
                    NamaPembeli = order.Username,
                    CreatedAt = order.OrderDate,
                    Products = order.OrderProducts.Select(op => new OrderProductDetailDto
                    {
                        ProductId = op.ProductId,
                        NamaProduk = op.Product.NamaProduk,
                        Harga = op.Product.Harga,
                        Quantity = op.Quantity
                    }).ToList()
                };

                result.Add(orderDetail);
            }
            return Ok(new{
                success = true,
                message = "Berhasil mendapatkan semua pesanan",
                data = result
            });
        }

        [HttpPost("sp")]
        [Authorize]
        public async Task<IActionResult> CreateOrderUsingStoredProcedure([FromBody] CreateOrderDto createOrderDto)
        {
            if (createOrderDto.Products == null || !createOrderDto.Products.Any())
            return BadRequest("Produk tidak boleh kosong");

            var result = await _orderRepo.CreateOrderUsingStoredProcedureAsync(
                createOrderDto.NamaPembeli,
                createOrderDto.Products.Select(p => new OrderProduct
                {
                    ProductId = p.ProductId,
                    Quantity = p.Quantity
                }).ToList()
            );

            if (result == "Sukses")
                return Ok(new { Message = "Order berhasil dibuat via Stored Procedure" });

            return BadRequest(new { Message = "Gagal membuat order" });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto createOrderDto)
        {
            try
            {
                var order = await _orderRepo.CreateOrderAsync(createOrderDto);

                if (order == null) return BadRequest("Gagal membuat pesanan, Masukkan data yang benar");

                return CreatedAtAction(nameof(GetOrderById), new
                {
                    id = order.Id
                }, new
                {
                    success = true,
                    message = "Pesanan berhasil dibuat",
                    data = new OrderDetailDto
                    {
                        Id = order.Id,
                        NamaPembeli = order.Username,
                        CreatedAt = order.OrderDate,
                        Products = order.OrderProducts.Select(op => new OrderProductDetailDto
                        {
                            ProductId = op.ProductId,
                            NamaProduk = op.Product.NamaProduk,
                            Harga = op.Product.Harga,
                            Quantity = op.Quantity
                        }).ToList()
                    }
                });
            }
            catch (Exception e)
            {   
                return BadRequest(new
                {
                    success = false,
                    message = "Gagal membuat pesanan",
                    error = e.Message
                });
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderRepo.GetOrderByIdAsync(id);
            if (order == null) return NotFound();

            var result = new OrderDetailDto
            {
                Id = order.Id,
                NamaPembeli = order.Username,
                CreatedAt = order.OrderDate,
                Products = order.OrderProducts.Select(op => new OrderProductDetailDto
                {
                    ProductId = op.ProductId,
                    NamaProduk = op.Product.NamaProduk,
                    Harga = op.Product.Harga,
                    Quantity = op.Quantity
                }).ToList()
            };

            return Ok(new {
                success = true,
                message = "Berhasil mendapatkan pesanan berdasarkan id",
                data = result
            });
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _orderRepo.DeleteOrderAsync(id);
            if (order == null) return NotFound();

            return Ok(new
            {
                success = true,
                message = "Pesanan berhasil dihapus"
            });
        }
    }
}