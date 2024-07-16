using Delivery.Email.Infra.Repositorio;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Core.Modelo
{
    public class PessoaFisica
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } = null;

        public string nomeCompleto { get; set; } = null!;

        public string CPF { get; set; }

        public DateTime dataNascimento { get; set; }

        public string email { get; set; } = null!;

        public Endereco DadosEndereco { get; set; }
    }
}
