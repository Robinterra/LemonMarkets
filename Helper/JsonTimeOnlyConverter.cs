using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LemonMarkets.Helper
{

    public class JsonTimeOnlyConverter : JsonConverter<TimeOnly>
    {
        private readonly string serializationFormat;

        public JsonTimeOnlyConverter() : this(null)
        {

        }

        public JsonTimeOnlyConverter(string? serializationFormat)
        {
            this.serializationFormat = serializationFormat ?? "HH:mm:ss.fff";
        }

        public override TimeOnly Read(ref Utf8JsonReader reader, 
            Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            return TimeOnly.Parse(value!);
        }

        public override void Write(Utf8JsonWriter writer, TimeOnly value, 
            JsonSerializerOptions options)
            => writer.WriteStringValue(value.ToString(serializationFormat));
    }

}