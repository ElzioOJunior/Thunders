namespace ThundersTeste.Infra.CrossCutting.Environments.Configurations;

public class MongoDbConfiguration
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
    public Collections Collections { get; set; }
}

public class Collections
{
    public string Tarefa { get; set; }
}
