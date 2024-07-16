using Delivery.api.Controllers;
using Delivery.Core.Modelo;
using Delivery.Email.Worker.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq.AutoMock;
using System.Diagnostics.CodeAnalysis;
using Moq;

namespace Delivery.Cadastro.API.Tests.Controllers
{
    [ExcludeFromCodeCoverage]
    public class CadastroControllerTests
    {
        private readonly AutoMocker _mocker;
        private readonly CadastroController _controller;

        public CadastroControllerTests()
        {
            _mocker = new AutoMocker();
            _controller = _mocker.CreateInstance<CadastroController>();
        }

        [Fact(DisplayName = "Cadastro Controller Construtor Retorna Instância")]
        [Trait("Categoria", "Delivery Cadastro API - Cadastro Controller")]
        public void CadastroController_Construtor_RetornaInstancia()
        {
            // Arrange
            var massTransit = _mocker.GetMock<IMessageBus>();
            var logger = _mocker.GetMock<ILogger<CadastroController>>();

            // Act
            var controller = new CadastroController(logger.Object, massTransit.Object);

            // Assert
            Assert.NotNull(controller);
        }

        [Fact(DisplayName = "Cadastro Controller Cadastrar Usuário Retorna Status 201")]
        [Trait("Categoria", "Delivery Cadastro API - Cadastro Controller")]
        public void CadastroController_CadastrarUsuario_Retorna201()
        {
            // Arrange
            var dados = _mocker.CreateInstance<PessoaFisica>();

            // Act
            var resultado = _controller.CadastrarUsuário(dados);

            // Assert
            var createdResult = Assert.IsType<StatusCodeResult>(resultado);
            Assert.Equal(201, createdResult.StatusCode);
        }

        [Fact(DisplayName = "Cadastro Controller Cadastrar Usuário Retorna Status 400 Quando Erro")]
        [Trait("Categoria", "Delivery Cadastro API - Cadastro Controller")]
        public void CadastroController_CadastrarUsuario_Retorna400QuandoErro()
        {
            // Arrange
            var dados = _mocker.CreateInstance<PessoaFisica>();
            var massTransit = _mocker.GetMock<IMessageBus>().Setup(m =>
                m.Consumer(It.IsAny<PessoaFisica>())).Throws(new InvalidOperationException("Teste"));


            // Act
            var resultado = _controller.CadastrarUsuário(dados);

            // Assert
            var createdResult = Assert.IsType<BadRequestResult>(resultado);
            Assert.Equal(400, createdResult.StatusCode);
        }
    }
}
