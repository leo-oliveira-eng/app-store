using Catalog.Domain.Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using System;
using Model = Catalog.Domain.Authors.Models;

namespace Catalog.Infrastructure.Mappings
{
    public static class EntityMapping
    {
        public static void MapEntity()
        {
            BsonClassMap.RegisterClassMap<Entity>(cm =>
            {
                BsonSerializer.RegisterSerializer(typeof(Guid), new GuidSerializer(BsonType.String));

                cm.AutoMap();

                cm.MapIdMember(x => x.Id);

                cm.MapMember(x => x.CreatedAt).SetDefaultValue(DateTime.Now);

                cm.MapMember(x => x.LastUpdate).SetDefaultValue(DateTime.Now);
            });
        }
    }
}
