using Delivery.Email.Infra.Repositorio;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Delivery.Email.Core.Domain
{
    public class PessoaFisica
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string nomeCompleto { get; set; } = null!;

        public string CPF { get; set; }

        public DateTime dataNascimento { get; set; }

        public string email { get; set; } = null!;

        public Endereco DadosEndereco { get; set; }
    }


}
