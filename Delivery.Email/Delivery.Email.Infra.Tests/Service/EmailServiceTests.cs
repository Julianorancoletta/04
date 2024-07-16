using Delivery.Email.Core.Domain;
using Delivery.Email.Infra.Connections;
using Delivery.Email.Infra.Service;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Moq;
using Moq.AutoMock;
using System.Diagnostics.CodeAnalysis;

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

        [Fact(DisplayName = "Pessoa Repositório Deve Retornar Todas Entidades Quando Existir")]
        [Trait("Categoria", "Delivery Cadastro Data - Pessoa Repositorio")]
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
