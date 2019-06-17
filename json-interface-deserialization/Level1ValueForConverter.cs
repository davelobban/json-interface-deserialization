using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace json_interface_deserialization
{
    public class Level1ValueForConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value, typeof(Level1ValueFor));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var item = JObject.Load(reader);
            if (item["Typ"].Value<string>() == "Level1ValueFor")
            {
                return new Level1ValueForFactory().GetNew(item);
            }
            var value = serializer.Deserialize(reader, typeof(Level1ValueFor)); 
            
            return value;
        }

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(Level1ValueFor));
        }

    }
}