using Cadastro_API.Core.Interface.Service;
using Cadastro_API.Core.Modelo;
using Microsoft.AspNetCore.Mvc;

namespace Cadastro_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CadastroController : ControllerBase
    {

        private readonly ILogger<CadastroController> _logger;
        private readonly ICadastroService _CadService;

        public CadastroController(ILogger<CadastroController> logger, ICadastroService cadastroService)
        {
            _logger = logger;
            _CadService = cadastroService;
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarUsuário(DadosCadastrais dados)
        {
            try
            {
                await _CadService.Cadastrando(dados);
                return StatusCode(201);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest();
            }
            
        }
    }
}