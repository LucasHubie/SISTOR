using SISTOR.Configuration;
using SISTOR.Controllers;
using SISTOR.Interfaces;
using SISTOR.Models;
using SISTOR.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SISTOR.Repository
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly AppDbContext _context;
        public UsuarioRepositorio(AppDbContext context) 
        {
            _context = context;
        }

        public AppDbContext AppDbContext
        {
            get { return _context as AppDbContext; }
        }

        public Usuario CriarUsuario(Usuario user)
        {
            if(user != null)
            {
                user.Senha = Md5Hash.CalculaHash(user.Senha);
                _context.Add(user);
                _context.SaveChanges();
            }
            return user;
        }

        public Usuario GetUsuario(int? id)
        {
            return _context.Usuario.Where(p => p.Id == id).FirstOrDefault();
        }

        public Usuario GetUsuarioLogin(Usuario u)
        {
            return AppDbContext.Usuario.Where(a => a.Login.Equals(u.Login)).FirstOrDefault();
        }
    }
}
