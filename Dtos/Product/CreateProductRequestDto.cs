using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace sistem_pemesanan_produk.Dtos.Product
{
    public class CreateProductRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Nama produk harus lebih dari 3 karakter")]
        [MaxLength(50, ErrorMessage = "Nama produk tidak boleh lebih dari 50 karakter")]
        public string NamaProduk { get; set; } = string.Empty;
        [MaxLength(200, ErrorMessage = "Deskripsi produk tidak boleh lebih dari 200 karakter")]
        public string Deskripsi { get; set; } = string.Empty;
        [Required]
        [Range(1, 1000000, ErrorMessage = "Harga produk harus lebih dari 0")]
        public decimal Harga { get; set; }
    }
}