using SISTOR.Configuration;
using SISTOR.Interfaces;
using SISTOR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SISTOR.Repository
{
    public class OrdemServicoRepositorio : IOrdemServicoRepositorio
    {
        private readonly AppDbContext _context;

        public OrdemServicoRepositorio(AppDbContext context)
        {
            _context = context;
        }

        public OrdemServico GetOrdemServioById(int id)
        {
            return _context.OrdemServico.Where(p => p.Id == id).FirstOrDefault();
        }

    }
}
