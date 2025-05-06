using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sistem_pemesanan_produk.Dtos.Product;
using sistem_pemesanan_produk.Models;

namespace sistem_pemesanan_produk.Mappers
{
    public static class ProductMapper
    {
        public static ProductDto ToProductDto(this Product productModel)
        {
            return new ProductDto
            {
                Id = productModel.Id,
                NamaProduk = productModel.NamaProduk,
                Deskripsi = productModel.Deskripsi,
                Harga = productModel.Harga
            };
        }

        public static Product ToProductFromCreateDto(this CreateProductRequestDto productDto)
        {
            return new Product
            {
                NamaProduk = productDto.NamaProduk,
                Deskripsi = productDto.Deskripsi,
                Harga = productDto.Harga
            };
        }
    }
}