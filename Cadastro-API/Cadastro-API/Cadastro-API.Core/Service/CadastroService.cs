using Cadastro_API.Core.Interface.Repositorio;
using Cadastro_API.Core.Interface.Service;
using Cadastro_API.Core.Modelo;

namespace Cadastro_API.Core.Service
{
    public class CadastroService : ICadastroService
    {
        private readonly ICadastroRepositorio _CadastroRepositorio;
        public CadastroService(ICadastroRepositorio cadastroRepositorio)
        {
            _CadastroRepositorio = cadastroRepositorio;
        }

        public async Task Cadastrando(DadosCadastrais dados)
        {
            await _CadastroRepositorio.AdicionarFila(dados);
        }
    }
}
