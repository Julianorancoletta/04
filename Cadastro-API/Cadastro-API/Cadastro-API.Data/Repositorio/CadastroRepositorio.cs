using Cadastro_API.Core.Interface.Repositorio;
using Cadastro_API.Core.Modelo;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Cadastro_API.Data.Repositorio
{
    public class CadastroRepositorio : ICadastroRepositorio
    {
        public async Task AdicionarFila(DadosCadastrais dados)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    var mensagem = JsonSerializer.Serialize(dados);

                    var corpo = Encoding.UTF8.GetBytes(mensagem);

                    channel.BasicPublish(
                        exchange: "",       //como não tem nada ele está pegando de ponto a ponto que é o padrão.
                        routingKey: "pessoa_queue", //se não tiver nenhuma Key criada então é assumido o nome da fila.
                        basicProperties: null, //É o padrão.
                        body: corpo
                        );
                }
            }
        }
    }
}
