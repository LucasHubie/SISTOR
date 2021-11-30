using SISTOR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SISTOR.Interfaces
{
    public interface IUsuarioRepositorio
    {
        Usuario CriarUsuario(Usuario user);
        Usuario GetUsuario(int? id);
        Usuario GetUsuarioLogin(Usuario u);
    }
}
