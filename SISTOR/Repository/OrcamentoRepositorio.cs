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
    public class OrcamentoRepositorio : IOrcamentoRepositorio
    {
        private readonly AppDbContext _context;
        public OrcamentoRepositorio(AppDbContext context) 
        {
            _context = context;
        }

        public AppDbContext AppDbContext
        {
            get { return _context as AppDbContext; }
        }

        public Orcamento CriarOrcamento(Orcamento user)
        {
            if(user != null)
            {
                _context.Add(user);
                _context.SaveChanges();
            }
            return user;
        }

    }
}
