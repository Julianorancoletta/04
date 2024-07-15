using Delivery.Core.Modelo;
using Delivery.Email.Worker.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CadastroController : ControllerBase
    {

        private readonly ILogger<CadastroController> _logger;
        private readonly IMessageBus _CadService;

        public CadastroController(ILogger<CadastroController> logger, IMessageBus cadastroService)
        {
            _logger = logger;
            _CadService = cadastroService;
        }

        [HttpPost]
        public IActionResult CadastrarUsuário(PessoaFisica dados)
        {
            try
            {
                _CadService.Consumer(dados);
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