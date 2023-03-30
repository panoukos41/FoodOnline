using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace FoodOnline.Commons.BsonSerializesrs;

public sealed class UuidBsonSerializer : SerializerBase<Uuid>
{
    public static bool TryRegister() => BsonSerializer.TryRegisterSerializer(new UuidBsonSerializer());

    public override Uuid Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var raw = context.Reader.ReadString();
        return Uuid.Parse(raw);
    }

    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Uuid value)
    {
        context.Writer.WriteString(value.ToString());
    }
}
