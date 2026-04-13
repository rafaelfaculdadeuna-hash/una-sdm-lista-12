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
    public class ClientesController : ControllerBase
    {
       private readonly AppDbContext _context;
        public ClientesController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var clientes = _context.Clientes.ToList();
            return Ok(clientes);
        }

        [HttpPost]
        public IActionResult Post(Cliente cliente)
        {
           
                _context.Clientes.Add(cliente);
                _context.SaveChanges();
                return CreatedAtAction(nameof(Get), new{id=cliente.Id},cliente);  
                      
            
        } 
    }
}