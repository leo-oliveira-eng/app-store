using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Catalog.Infrastructure.Author.Documents
{
    [BsonDiscriminator(Required = false)]
    public class AuthorDocument
    {
        [BsonElement("_id")]
        public ObjectId Id { get; set; }

        [BsonElement("code")]
        public Guid Code { get; set; }

        [BsonElement("createdAt")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreatedAt { get; set; }

        [BsonElement("lastUpdate")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime LastUpdate { get; set; }

        [BsonElement("deletedAt")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? DeletedAt { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("cnpj")]
        public string CNPJ { get; set; }

        [BsonElement("phoneNumber")]
        public string PhoneNumber { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("webSite")]
        public string WebSite { get; set; }

        [BsonElement("brandLogo")]
        public string BrandLogo { get; set; }
    }
}
