using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SISTOR.Configuration;
using SISTOR.Interfaces;
using SISTOR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SISTOR.Interfaces.IClienteRepositorio;

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


        //public IActionResult Index()
        //{
        //    List<Cliente> cliente = _clienteRepositorio.GetClientes();
            
        //    return Json(cliente);
        //}

        public IActionResult Index(int pageNumber, int pageSize)
        {
            //List<Cliente> cliente = _clienteRepositorio.GetClientes();


            retornoClientes retorno = _clienteRepositorio.GetClientes(pageNumber, pageSize);
            return Json(new { lst = retorno.lst, qntdRegistros = retorno.qntdRegistros });
        }


        [HttpPost]
        public IActionResult Create([FromBody]Cliente cliente)
        {
            bool sucess = true;
            var description = "";
            cliente.Id = 0;
            cliente.IdPessoa = 0;

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

        [HttpPost]
        public IActionResult Update([FromBody] Cliente cliente)
        {
            bool sucess = true;
            var description = "";

            try
            {
                _clienteRepositorio.AtualizarCliente(cliente);
                sucess = true;
                description = "Cliente atualizado com sucesso!";
                return Json(new { sucess = sucess, description = description });
            }
            catch (Exception ex)
            {
                sucess = false;
                description = "Cliente não atualizado";
                return Json(new { sucess = sucess, description = ex.Message });
            }
        }

        public IActionResult Excluir(int id)
        {
            bool sucess = true;
            var description = "";
            try
            {
                var cliente = _clienteRepositorio.GetClienteById(id);
                if (cliente != null)
                {
                    _clienteRepositorio.Delete(cliente.Id);
                    sucess = true;
                    description = "Cliente excluido com sucesso!";
                    return Json(new { sucess = sucess, description = description });
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
                description = "Cliente não excluido";
                return Json(new { sucess = sucess, description = ex.Message });
            }
        }

        public IActionResult GetClienteById(int id)
        {
            bool sucess = true;
            var description = "";

            try
            {
                var cliente = _clienteRepositorio.GetClienteById(id);
                sucess = true;
                description = "Cliente encontrado com sucesso!";
                return Json(new { sucess = sucess, description = description, cliente = cliente });
            }
            catch (Exception ex)
            {
                sucess = false;
                description = "Cliente não encontrado criado";
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

        public IActionResult buscaCliente(string busca)
        {
            bool sucess = true;
            var description = "";

            try
            {
                List<Cliente> obj = new List<Cliente>();
                obj = _clienteRepositorio.buscaCliente(busca);
                if (obj != null)
                {
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
