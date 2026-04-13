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
    [Route("Api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly AppDbContext _context;
        public PedidosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var pedidos = _context.Pedidos.ToList();
            return Ok(pedidos);
        }

        [HttpPost]
        public IActionResult Post(Pedido pedido)
        {

            var produto = _context.Produtos.Find(pedido.ProdutoId);
            if (produto.QuantidadeEstoque < pedido.QuantidateItens)
            {
                return Conflict("Estoque insuficiente para este modelo.");
            }
            else
            {
                if (produto.Nome == "Air Jordan")
                {
                    Console.WriteLine("Alerta de Hype: Um Air Jordan acaba de ser vendido!");
                }
                produto.QuantidadeEstoque = produto.QuantidadeEstoque - pedido.QuantidateItens;
                _context.Pedidos.Add(pedido);
                _context.SaveChanges();
                return CreatedAtAction(nameof(Get), new { id = pedido.Id }, pedido);
            }

        }

        [HttpGet("pedido/{id}")]
        public IActionResult GetDetalhado(int id)
        {
            var pedidos = _context.Pedidos.Find(id);
            var produto = _context.Produtos.Find(pedidos.ProdutoId);
            var cliente = _context.Clientes.Find(pedidos.ClienteId);

            return Ok($"Pedido numero: {id}, Cliente responsavel: {cliente.NomeCompleto}, Produto vendido: {produto.Nome}");
        }


    }
}