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
    [Route("Api/[controller]/balanco")]
    public class AdminController : ControllerBase
    {
        private readonly AppDbContext _context;
        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("faturamento")]
        public IActionResult FaturamentoTotal()
        {
            var pedidos = _context.Pedidos.ToList();
            decimal faturamentoTotal = 0;

            foreach (var pedido in pedidos)
            {
                var produto = _context.Produtos.Find(pedido.ProdutoId);

                if (produto != null)
                {
                    faturamentoTotal += produto.Preco * pedido.QuantidateItens;
                }
            }

            return Ok(new { faturamentoTotal });

        }
        [HttpGet("giro-estoque")]
        public IActionResult GiroEstoque()
        {
            var pedidos = _context.Pedidos.ToList();
            var produtosSemEstoque = new List<Produto>();
            
            foreach (var pedido in pedidos)
            {
                var produto = _context.Produtos.Find(pedido.ProdutoId);

                if (produto != null && produto.QuantidadeEstoque == 0)
                {
                    produtosSemEstoque.Add(produto);
                }
            }

            return Ok(produtosSemEstoque.Distinct());
        }
    }
}