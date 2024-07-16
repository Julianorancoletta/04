using Delivery.Email.Core.Domain;
using Delivery.Email.Infra.Connections;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Delivery.Email.Infra.Service
{
    public class EmailService
    {
        private readonly IMongoCollection<PessoaFisica> _EmailCollection;

        public EmailService(IMongoClient client, IOptions<PessoaDatabaseSettings> EmailDatabaseSettings)
        {
            var mongoDatabase = client.GetDatabase(EmailDatabaseSettings.Value.DatabaseName);

            _EmailCollection = mongoDatabase.GetCollection<PessoaFisica>(
                EmailDatabaseSettings.Value.PessoaCollectionName);
        }

        public async Task<List<PessoaFisica>> GetAsync() =>
            await _EmailCollection.Find(_ => true).ToListAsync();

        public async Task<PessoaFisica?> GetAsync(string id) =>
            await _EmailCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(PessoaFisica newBook) =>
            await _EmailCollection.InsertOneAsync(newBook);

        public async Task UpdateAsync(string id, PessoaFisica updatedBook) =>
            await _EmailCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);

        public async Task RemoveAsync(string id) =>
            await _EmailCollection.DeleteOneAsync(x => x.Id == id);
    }
}
