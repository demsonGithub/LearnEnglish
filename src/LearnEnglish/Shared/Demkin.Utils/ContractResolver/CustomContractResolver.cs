using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Demkin.Utils.ContractResolver
{
    public class CustomContractResolver : CamelCasePropertyNamesContractResolver
    {
        /// <summary>
        /// 对长整型做处理
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        protected override JsonConverter ResolveContractConverter(Type objectType)
        {
            if (objectType == typeof(long))
            {
                return new JsonConverterLong();
            }
            return base.ResolveContractConverter(objectType);
        }
    }

    public class JsonConverterLong : JsonConverter
    {
        /// <summary>
        /// 是否可以转换
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            if ((reader.ValueType == null || reader.ValueType == typeof(long?)) && reader.Value == null)
            {
                return null;
            }
            else
            {
                long.TryParse(reader.Value != null ? reader.Value.ToString() : "", out long value);
                return value;
            }
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            if (value == null)
                writer.WriteValue(value);
            else
                writer.WriteValue(value + "");
        }
    }
}