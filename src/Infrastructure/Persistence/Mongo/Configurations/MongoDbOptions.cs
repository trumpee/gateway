namespace Infrastructure.Persistence.Mongo.Configurations;

public class MongoDbOptions
{
    public static string ConfigurationSectionName =>
        nameof(MongoDbOptions).Replace("Options", string.Empty);

    public string DatabaseName { get; set; } = null!;
    public string ConnectionString { get; set; } = null!;
}