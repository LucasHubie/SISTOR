using SISTOR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SISTOR.Service
{
    public class UFService
    {
        public static List<UF> GetEstados()
        {
            var listaEstados = new List<UF>()
            {
                new UF(){ EstadoSigla="AL", EstadoNome="Alagoas"},
                new UF(){ EstadoSigla="AC", EstadoNome="Acre"},
                new UF(){ EstadoSigla="AP", EstadoNome="Amapa"},
                new UF(){ EstadoSigla="AM", EstadoNome="Amazonas"},
                new UF(){ EstadoSigla="BA", EstadoNome="Bahia"},
                new UF(){ EstadoSigla="CE", EstadoNome="Ceará"},
                new UF(){ EstadoSigla="DF", EstadoNome="Distrito Federal"},
                new UF(){ EstadoSigla="ES", EstadoNome="Espirito Santo"},
                new UF(){ EstadoSigla="GO", EstadoNome="Goias"},
                new UF(){ EstadoSigla="MA", EstadoNome="Maranhao"},
                new UF(){ EstadoSigla="MT", EstadoNome="Mato Grosso"},
                new UF(){ EstadoSigla="MS", EstadoNome="Mato Grosso do Sul"},
                new UF(){ EstadoSigla="MG", EstadoNome="Minas Gerais"},
                new UF(){ EstadoSigla="PR", EstadoNome="Parana"},
                new UF(){ EstadoSigla="PB", EstadoNome="Paraiba"},
                new UF(){ EstadoSigla="PA", EstadoNome="Para"},
                new UF(){ EstadoSigla="PE", EstadoNome="Pernambuco"},
                new UF(){ EstadoSigla="RJ", EstadoNome="Rio de Janeiro"},
                new UF(){ EstadoSigla="RS", EstadoNome="Rio Grande do Sul"},
                new UF(){ EstadoSigla="RR", EstadoNome="Roraima"},
                new UF(){ EstadoSigla="RO", EstadoNome="Rondonia"},
                new UF(){ EstadoSigla="SP", EstadoNome="São Paulo"},
                new UF(){ EstadoSigla="SC", EstadoNome="Santa Catarina"},
                new UF(){ EstadoSigla="SE", EstadoNome="Sergipe"},
                new UF(){ EstadoSigla="TO", EstadoNome="Tocantins"}
            };
            return listaEstados;
        }
    }
}
