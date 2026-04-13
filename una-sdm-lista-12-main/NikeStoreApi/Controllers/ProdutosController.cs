using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NikeStoreApi.Data;
using NikeStoreApi.Models;

namespace NikeStoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var produtos = _context.Produtos.ToList();
            return Ok(produtos);
        }

        [HttpPost]
        public IActionResult Post(Produto produto)
        {
            if (produto.Preco <= 0)
            {
                return BadRequest("O preço não pode ser igual o menor a 0");
            }
            else
            {
                _context.Produtos.Add(produto);
                _context.SaveChanges();
                return CreatedAtAction(nameof(Get), new { id = produto.Id }, produto);
            }

        }
    }
}