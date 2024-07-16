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

    public class Endereco
    {
        public string Rua { get; set; }
        public string Bairro { get; set; }
        public int NumeroResidencia { get; set; }
        public string Complemento { get; set; }
        public int CEP { get; set; }
    }
}
