using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sistem_pemesanan_produk.Dtos.Product;
using sistem_pemesanan_produk.Interfaces;
using sistem_pemesanan_produk.Mappers;

namespace sistem_pemesanan_produk.Controllers
{
    [Route("api/produk")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepo;
        public ProductController(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var product = await _productRepo.GetAllAsync();
            var productDto = product.Select(p => p.ToProductDto());
            
            return Ok(new
            {
                success = true,
                message = "Berhasil mendapatkan semua produk",
                data = productDto
            });
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var product = await _productRepo.GetByIdAsync(id);
            if (product == null) return NotFound(
                new
                {
                    success = false,
                    message = "Produk tidak ditemukan"
                }
            );

            return Ok(new
            {
                success = true,
                message = "Berhasil mendapatkan produk",
                data = product.ToProductDto()
            });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateProductRequestDto productDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var productModel = productDto.ToProductFromCreateDto();
            await _productRepo.CreateAsync(productModel);

            return CreatedAtAction(nameof(GetAll), new 
            { 
                success = true,
                message = "Berhasil menambahkan produk",
                data = productModel.ToProductDto()
            });
        }

        [HttpPut]
        [Route("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateProductRequestDto prdouctDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var productModel = await _productRepo.UpdateAsync(id, prdouctDto);
            if (productModel == null) return NotFound(new
            {
                success = false,
                message = "Produk tidak ditemukan"
            });

            return Ok(new {
                success = true,
                message = "Berhasil memperbarui produk",
                data = productModel.ToProductDto()
            });
        }   

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            var productModel = await _productRepo.DeleteAsync(id);
            if (productModel == null) return NotFound(
                new
                {
                    success = false,
                    message = "Produk tidak ditemukan"
                }
            );

            return Ok(new
            {
                success = true,
                message = "Berhasil menghapus produk"
            });
        } 
    }
}