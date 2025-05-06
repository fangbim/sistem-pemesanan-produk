using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using sistem_pemesanan_produk.Data;
using sistem_pemesanan_produk.Dtos.Product;
using sistem_pemesanan_produk.Interfaces;
using sistem_pemesanan_produk.Models;

namespace sistem_pemesanan_produk.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDBContext _context;
        public ProductRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Product> CreateAsync(Product productModel)
        {
            await _context.Products.AddAsync(productModel);
            await _context.SaveChangesAsync();

            return productModel;
        }

        public async Task<Product?> DeleteAsync(int id)
        {
            var productModel = await _context.Products.FindAsync(id);
            if (productModel == null) return null;

            _context.Products.Remove(productModel);
            await _context.SaveChangesAsync();
            
            return productModel;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product?> UpdateAsync(int id, UpdateProductRequestDto productDto)
        {
            var existingProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if ( existingProduct == null ) return null;

            existingProduct.NamaProduk = productDto.NamaProduk;
            existingProduct.Deskripsi = productDto.Deskripsi;
            existingProduct.Harga = productDto.Harga;

            await _context.SaveChangesAsync();
            return existingProduct;
        }
    }
}