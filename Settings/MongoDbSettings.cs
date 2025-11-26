namespace MecanicApp.Api.Settings
{
    public class MongoDbSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
        public string UsuariosCollectionName { get; set; } = string.Empty;
    }
}
