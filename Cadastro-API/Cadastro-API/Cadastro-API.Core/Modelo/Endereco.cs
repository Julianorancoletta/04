using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadastro_API.Core.Modelo
{
    public class Endereco
    {
        public string Rua { get; set; }
        public string Bairro { get; set; }
        public int NumeroResidencia { get; set; }
        public string Complemento { get; set; }
        public int CEP { get; set; }
    }
}
