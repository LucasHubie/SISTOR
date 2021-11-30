using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SISTOR.Configuration;
using SISTOR.Interfaces;
using SISTOR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SISTOR.Controllers
{
    public class ClienteController : Controller
    {
        private readonly AppDbContext _context;
        private IClienteRepositorio _clienteRepositorio;

        public ClienteController(AppDbContext context, IClienteRepositorio clienteRepositorio)
        {
            _context = context;
            _clienteRepositorio = clienteRepositorio;
        }


        public IActionResult Index()
        {
            List<Cliente> cliente = _clienteRepositorio.GetClientes();
            return Json(cliente);
        }

        
        [HttpPost]
        public IActionResult Create([FromBody]Cliente cliente)
        {
            bool sucess = true;
            var description = "";

           try { 
                _clienteRepositorio.CriarCliente(cliente);
                sucess = true;
                description = "Cliente criado com sucesso!";
                return Json(new { sucess = sucess, description = description });
            }
            catch(Exception ex)
            {
                sucess = false;
                description = "Cliente não criado";
                return Json(new { sucess = sucess, description = ex.Message });
            }
        }

        public IActionResult GetClienteByCPF(string cpf)
        {
            bool sucess = true;
            var description = "";

            try
            {
                _clienteRepositorio.GetClienteByCPF(cpf);
                sucess = true;
                description = "Cliente encontrado com sucesso!";
                return Json(new { sucess = sucess, description = description });
            }
            catch (Exception ex)
            {
                sucess = false;
                description = "Cliente não encontrado criado";
                return Json(new { sucess = sucess, description = ex.Message });
            }
        }

        public IActionResult GetClienteByNome(string nome)
        {
            bool sucess = true;
            var description = "";

            try
            {
                Cliente obj = new Cliente();
                obj = _clienteRepositorio.GetClienteByNome(nome);
                if (obj != null) { 
                sucess = true;
                description = "Cliente encontrado com sucesso!";
                return Json(new { sucess = sucess, description = description, cliente = obj });
                }
            else
            {
                sucess = false;
                description = "Cliente não encontrado";
                return Json(new { sucess = sucess, description = description });
            }
            }
            catch (Exception ex)
            {
                sucess = false;
                description = "Cliente não encontrado";
                return Json(new { sucess = sucess, description = ex.Message });
            }
        }
    }
}
