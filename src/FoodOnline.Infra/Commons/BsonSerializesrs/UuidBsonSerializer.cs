using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace FoodOnline.Commons.BsonSerializesrs;

public sealed class UuidBsonSerializer : SerializerBase<Uuid>
{
    public static bool TryRegister() => BsonSerializer.TryRegisterSerializer(new UuidBsonSerializer());

    public override Uuid Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        return Uuid.TryParse(context.Reader.ReadString(), out var uuid) ? uuid : Uuid.Empty;
    }

    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Uuid value)
    {
        context.Writer.WriteString(value.ToString());
    }
}
