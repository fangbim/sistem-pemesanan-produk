using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sistem_pemesanan_produk.Dtos.Product;
using sistem_pemesanan_produk.Models;

namespace sistem_pemesanan_produk.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task<Product> CreateAsync(Product productModel);
        Task<Product?> UpdateAsync(int id, UpdateProductRequestDto productDto);
        Task<Product?> DeleteAsync(int id);
    }
}