using Microsoft.AspNetCore.Mvc;
using SISTOR.Configuration;
using SISTOR.Interfaces;
using SISTOR.Models;
using SISTOR.Repository;
using SISTOR.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SISTOR.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly AppDbContext _context;
        private IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioController(AppDbContext context, IUsuarioRepositorio usuarioRepositorio)
        {
            _context = context;
            _usuarioRepositorio = usuarioRepositorio;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create(Usuario user)
        {
            bool sucess = true;
            var description = "";
            if(user != null)
            {
                _usuarioRepositorio.CriarUsuario(user);
                sucess = true;
                description = "Usuário criado com sucesso!";
                return Json(new { sucess = sucess, description = description });
            }
            else
            {
                sucess = false;
                description = "Usuário não criado";
                return Json(new { sucess = sucess, description = description });
            }
            
        }

        public IActionResult Login(Usuario user)
        {
            bool sucess = true;
            var description = "";
            var v = _usuarioRepositorio.GetUsuarioLogin(user);
            if (v != null)
            {
                user.Senha = Md5Hash.CalculaHash(user.Senha);
                if (v.Senha == user.Senha)
                {
                    sucess = true;
                    description = "Login realizado com sucesso";
                    return Json(new { sucess = sucess, description = description });
                }
                else
                {
                    sucess = false;
                    description = "Senha inválida";
                    return Json(new { sucess = sucess, description = description });
                }
            }
            else
            {
                sucess = false;
                description = "Login inválido";
                return Json(new { sucess = sucess, description = description });
            }
            
        }
    }
}
