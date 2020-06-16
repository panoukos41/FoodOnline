using FoodOnline.Domain.Stores.Catalogue;
using YamlDotNet.Serialization;

namespace FoodOnline.Domain.Models
{
    /// <summary>
    /// Use to get yaml serializer and deserializer configured for the
    /// catalogue data.
    /// </summary>
    public static class Yaml
    {
        /// <summary>
        /// Get a new <see cref="Serializer"/>
        /// </summary>
        public static ISerializer NewSerializer =>
            new SerializerBuilder()
            .WithTagMapping("!SelectSingle", typeof(SelectSingle))
            .WithTagMapping("!SelectMany", typeof(SelectMany))
            .Build();

        /// <summary>
        /// Get a new <see cref="Deserializer"/>
        /// </summary>
        public static IDeserializer NewDeserializer =>
            new DeserializerBuilder()
            .WithTagMapping("!SelectSingle", typeof(SelectSingle))
            .WithTagMapping("!SelectMany", typeof(SelectMany))
            .Build();
    }
}