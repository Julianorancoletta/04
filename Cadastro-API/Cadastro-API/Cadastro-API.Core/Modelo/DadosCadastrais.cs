using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadastro_API.Core.Modelo
{
    public class DadosCadastrais
    {
        public string NomeCompleto { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }
        public Endereco DadosEndereco { get; set; }
    }
}
