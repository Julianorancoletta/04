namespace Delivery.Cadastro.Infra.Connections
{
    public class PessoaDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string PessoaCollectionName { get; set; } = null!;
    }
}
