using MongoDB_NetCore_API.Entity;
using MongoDB_NetCore_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDB_NetCore_API.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
        public class ProductController : ControllerBase
        {
            private readonly IProductService _productService;

            public ProductController(IProductService inProductService)
            {
                _productService = inProductService;
            }

            [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                return Ok(await _productService.GetAllAsync());
            }

            [HttpGet("{id:length(24)}")]
            public async Task<IActionResult> Get(string id)
            {
                var book = await _productService.GetByIdAsync(id);
                if (book == null)
                {
                    return NotFound();
                }
                return Ok(book);
            }

            [HttpPost]
            public async Task<IActionResult> Create(Products product)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                await _productService.CreateAsync(product);
                return Ok(product);
            }

            [HttpPut("{id:length(24)}")]
            public async Task<IActionResult> Update(string id, Products productsData)
            {
                var product = await _productService.GetByIdAsync(id);
                if (product == null)
                {
                    return NotFound();
                }
                await _productService.UpdateAsync(id, productsData);
                return NoContent();
            }

            [HttpDelete("{id:length(24)}")]
            public async Task<IActionResult> Delete(string id)
            {
                var product = await _productService.GetByIdAsync(id);
                if (product == null)
                {
                    return NotFound();
                }
                await _productService.DeleteAsync(product.id);
                return NoContent();
            }
        }
    }
