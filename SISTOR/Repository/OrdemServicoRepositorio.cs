using Microsoft.EntityFrameworkCore;
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
        public List<OrdemServico> GetOrdemServico()
        {
            return _context.OrdemServico.Include("Cliente.Pessoa").Include("Funcionario.Pessoa").Include("Orcamento").ToList();
        }

        public OrdemServico GetOrdemServioById(int id)
        {
            return _context.OrdemServico.Where(p => p.Id == id).FirstOrDefault();
        }
        public int GetOrdemAberta()
        {
            int count = _context.OrdemServico.Where(x => (int)x.Situacao == 1).Count();
            return count;
        }
        public int GetOrdemFechada()
        {
            int count = _context.OrdemServico.Where(x => (int)x.Situacao == 2).Count();
            return count;
        }
    }
}
