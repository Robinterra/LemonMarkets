using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LemonMarkets.Helper
{

    public class JsonDecimalIntConverter : JsonConverter<decimal>
    {

        public JsonDecimalIntConverter()
        {

        }

        public override decimal Read(ref Utf8JsonReader reader, 
            Type typeToConvert, JsonSerializerOptions options)
        {
            int value = reader.GetInt32();

            return ((decimal)value) / 10000;
        }

        public override void Write(Utf8JsonWriter writer, decimal value, 
            JsonSerializerOptions options)
            => writer.WriteNumberValue((int)(value * 10000));
    }

}