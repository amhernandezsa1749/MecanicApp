using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MecanicApp.Api.Models
{
    public class Usuario
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("nombreCompleto")]
        public string NombreCompleto { get; set; } = string.Empty;

        [BsonElement("correo")]
        public string Correo { get; set; } = string.Empty;

        [BsonElement("password")]
        public string Password { get; set; } = string.Empty;

        [BsonElement("telefono")]
        public string Telefono { get; set; } = string.Empty;
    }
}
