using Cadastro_API.Core.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadastro_API.Core.Interface.Service
{
    public interface ICadastroService
    {
        Task Cadastrando(DadosCadastrais dados);
    }
}
