using Delivery.Email.Core.Domain;
using Delivery.Email.Infra.Connections;
using Delivery.Email.Infra.Service;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Moq;
using Moq.AutoMock;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Delivery.Email.Infra.Tests.Service
{
    [ExcludeFromCodeCoverage]
    public class EmailServiceTests
    {
        private readonly AutoMocker _mocker;
        private readonly EmailService _servico;

        public EmailServiceTests()
        {
            _mocker = new AutoMocker();
            var settings = new BookStoreDatabaseSettings
            {
                ConnectionString = "TestDatabase",
                BooksCollectionName = "Teste",
                DatabaseName = "Teste"
            };
            var opcoes = Options.Create(settings);
            _mocker.Use<IOptions<BookStoreDatabaseSettings>>(opcoes);

            var mockClient = _mocker.GetMock<IMongoClient>();
            var mockDatabase = _mocker.GetMock<IMongoDatabase>();
            var mockCollection = _mocker.GetMock<IMongoCollection<PessoaFisica>>();

            mockClient
                .Setup(c => c.GetDatabase(settings.DatabaseName, null))
                .Returns(mockDatabase.Object);
            mockDatabase
                .Setup(d => d.GetCollection<PessoaFisica>(settings.BooksCollectionName, null))
                .Returns(mockCollection.Object);

            _servico = new EmailService(mockClient.Object, opcoes);
        }

        [Fact(DisplayName = "E-mail Service Deve Retornar Todas Entidades Quando Existir")]
        [Trait("Categoria", "Delivery Cadastro Data - E-Mail Service")]
        public async Task GetAsync_DeveRetornarTodasEntidades_QuandoExistir()
        {
            // Arrange
            var dados = new PessoaFisica
            {
                nomeCompleto = "Teste Teste"
            };
            MockColecaoMongo(dados);

            // Act
            var resultado = await _servico.GetAsync();

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(dados.nomeCompleto, resultado.First()?.nomeCompleto);
        }

        [Fact(DisplayName = "E-mail Service Deve Retornar Entidade Com Mesmo Id Quando Passar Id")]
        [Trait("Categoria", "Delivery Cadastro Data - E-Mail Service")]
        public async Task GetAsync_DeveRetornarEntidadeComMesmoId_QuandoPassarId()
        {
            // Arrange
            var id = "1";
            var dados = new PessoaFisica
            {
                Id = id,
                nomeCompleto = "Teste Teste"
            };
            MockColecaoMongo(dados);

            // Act
            var resultado = await _servico.GetAsync(id);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(dados.Id, resultado.Id);
            Assert.Equal(dados.nomeCompleto, resultado.nomeCompleto);
        }

        [Fact(DisplayName = "E-mail Service Deve Gravar")]
        [Trait("Categoria", "Delivery Cadastro Data - E-Mail Service")]
        public async Task CreateAsync_DeveGravar_QuandoPassarEntidade()
        {
            // Arrange
            var dados = new PessoaFisica
            {
                nomeCompleto = "Teste Teste"
            };

            var mockColecao = _mocker.GetMock<IMongoCollection<PessoaFisica>>();
            mockColecao
                .Setup(c => c.InsertOneAsync(dados, null, default))
                .Returns(Task.CompletedTask);

            // Act
            await _servico.CreateAsync(dados);

            // Assert
            mockColecao.Verify(c => c.InsertOneAsync(dados, null, default),
                Times.Once);
        }

        [Fact(DisplayName = "E-mail Service Deve Atualizar")]
        [Trait("Categoria", "Delivery Cadastro Data - E-Mail Service")]
        public async Task UpdateAsync_DeveAtualizar_QuandoPassarEntidade()
        {
            // Arrange
            string dadoId = "1";
            var dadosAtualizar = new PessoaFisica
            {
                Id = dadoId,
                nomeCompleto = "Teste Novo"
            };

            var mockColecao = _mocker.GetMock<IMongoCollection<PessoaFisica>>();

            mockColecao
                .Setup(c => c.ReplaceOneAsync(It.IsAny<FilterDefinition<PessoaFisica>>(), dadosAtualizar, It.IsAny<ReplaceOptions>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ReplaceOneResult.Acknowledged(1, 1, null));

            // Act
            await _servico.UpdateAsync(dadoId, dadosAtualizar);

            // Assert
            mockColecao.Verify(
                c => c.ReplaceOneAsync(It.IsAny<FilterDefinition<PessoaFisica>>(), dadosAtualizar, It.IsAny<ReplaceOptions>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact(DisplayName = "E-mail Service Deve Remover")]
        [Trait("Categoria", "Delivery Cadastro Data - E-Mail Service")]
        public async Task RemoveAsync_DeveRemover_QuandoPassarId()
        {
            // Arrange
            string dadoId = "1";
            var mockColecao = _mocker.GetMock<IMongoCollection<PessoaFisica>>();

            mockColecao
                .Setup(c => c.DeleteOneAsync(It.IsAny<FilterDefinition<PessoaFisica>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new DeleteResult.Acknowledged(1));

            // Act
            await _servico.RemoveAsync(dadoId);

            // Assert
            mockColecao.Verify(
                c => c.DeleteOneAsync(It.IsAny<FilterDefinition<PessoaFisica>>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }

        private void MockColecaoMongo(PessoaFisica dados)
        {
            var mockColecao = _mocker.GetMock<IMongoCollection<PessoaFisica>>();
            var mockCursor = _mocker.GetMock<IAsyncCursor<PessoaFisica>>();

            mockCursor.Setup(_ => _.Current).Returns(new List<PessoaFisica> { dados });
            mockCursor.SetupSequence(_ => _.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);
            mockCursor.SetupSequence(_ => _.MoveNextAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(true)
                .ReturnsAsync(false);

            mockColecao
                .Setup(_ => _.FindAsync(
                    It.IsAny<FilterDefinition<PessoaFisica>>(),
                    It.IsAny<FindOptions<PessoaFisica, PessoaFisica>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockCursor.Object);
        }
    }
}
