using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LemonMarkets.Helper
{

    public class JsonUnixDateTimeConverter : JsonConverter<DateTime>
    {

        private static readonly DateTime _epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public JsonUnixDateTimeConverter()
        {

        }

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Number)
            {
                long time = reader.GetInt64();

                return _epoch.AddMilliseconds( time );
            }
            if (reader.TokenType == JsonTokenType.String)
            {
                DateTime result = reader.GetDateTime();

                return result;
            }

            return DateTime.MinValue;
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value);
        }
    }

}