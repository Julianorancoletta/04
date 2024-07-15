using Delivery.Cadastro.Infra.Connections;
using Delivery.Core.Modelo;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Delivery.Email.Infra.Service
{
    public class PessoaRepositorio
    {
        private readonly IMongoCollection<PessoaFisica> _EmailCollection;

        public PessoaRepositorio(IOptions<PessoaDatabaseSettings> options)
        {
            var mongoDatabase = new MongoClient(options.Value.ConnectionString)
                .GetDatabase(options.Value.DatabaseName); 

            _EmailCollection = mongoDatabase.GetCollection<PessoaFisica>(
                options.Value.PessoaCollectionName);
        }

        public async Task<List<PessoaFisica>> GetAsync() =>
            await _EmailCollection.Find(_ => true).ToListAsync();

        public async Task<PessoaFisica?> GetAsync(string id) =>
            await _EmailCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(PessoaFisica pessoaFisica) =>
            await _EmailCollection.InsertOneAsync(pessoaFisica);

        public async Task UpdateAsync(string id, PessoaFisica pessoaFisica) =>
            await _EmailCollection.ReplaceOneAsync(x => x.Id == id, pessoaFisica);

        public async Task RemoveAsync(string id) =>
            await _EmailCollection.DeleteOneAsync(x => x.Id == id);
    }
}
