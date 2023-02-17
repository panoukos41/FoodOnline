using FoodOnline.Common;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace FoodOnline.Infrastructure.Serializesrs.Bson;

public sealed class RoleBsonSerializer : SerializerBase<Role>
{
    public static bool TryRegister() => BsonSerializer.TryRegisterSerializer(new RoleBsonSerializer());

    public override Role Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var raw = context.Reader.ReadString();
        return Role.FromName(raw, true);
    }

    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Role value)
    {
        context.Writer.WriteString(value.Name);
    }
}
