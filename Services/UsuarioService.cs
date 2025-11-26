using MecanicApp.Api.Models;
using MecanicApp.Api.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MecanicApp.Api.Services
{
    public class UsuarioService
    {
        private readonly IMongoCollection<Usuario> _usuariosCollection;

        public UsuarioService(IOptions<MongoDbSettings> mongoSettings)
        {
            var settings = mongoSettings.Value;

            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _usuariosCollection = database.GetCollection<Usuario>(settings.UsuariosCollectionName);
        }

        public async Task<bool> CorreoExisteAsync(string correo)
        {
            var filter = Builders<Usuario>.Filter.Eq(u => u.Correo, correo);
            var count = await _usuariosCollection.CountDocumentsAsync(filter);
            return count > 0;
        }

        public async Task CrearUsuarioAsync(Usuario usuario)
        {
            await _usuariosCollection.InsertOneAsync(usuario);
        }

        public async Task<Usuario?> LoginAsync(string correo, string password)
        {
            var filter = Builders<Usuario>.Filter.And(
                Builders<Usuario>.Filter.Eq(u => u.Correo, correo),
                Builders<Usuario>.Filter.Eq(u => u.Password, password)
            );

            return await _usuariosCollection
                .Find(filter)
                .FirstOrDefaultAsync();
        }
    }
}
