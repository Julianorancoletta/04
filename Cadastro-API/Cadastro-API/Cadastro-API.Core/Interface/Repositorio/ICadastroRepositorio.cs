using Cadastro_API.Core.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadastro_API.Core.Interface.Repositorio
{
    public interface ICadastroRepositorio
    {
        Task AdicionarFila(DadosCadastrais dados);
    }
}
