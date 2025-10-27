using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FinancasPessoais.Api.Converters
{
    public class CustomDateTimeConverter : JsonConverter<DateTime>
    {
        #region fonte do exemplo
        //https://makolyte.com/csharp-changing-the-json-serialization-date-format/
        //https://makolyte.com/system-text-json-use-jsonconverterfactory-to-serialize-multiple-types-the-same-way/
        #endregion

        private readonly string Format;
        public CustomDateTimeConverter(string format)
        {
            Format = format;
        }

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.ParseExact(reader.GetString(), Format, null);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(Format));
        }
    }
}
