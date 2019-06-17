using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace json_interface_deserialization
{
    public class Level0ValueForConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value, typeof(Level0ValueFor));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var item = JObject.Load(reader);
            if (item["Typ"].Value<string>() == "Level0ValueFor")
            {
                switch (item["_iValueFor"].Value<JObject>()["Typ"].Value<string>())
                {
                    case "EnvironmentPrinter":
                        return new Level0ValueFor(new EnvironmentPrinter());
                    case "HappyEnvironmentPrinter":
                        return new Level0ValueFor(new HappyEnvironmentPrinter());
                    case "Level1ValueFor":
                        return new Level0ValueFor(
                            new Level1ValueForFactory().GetNew(item["_iValueFor"].Value<JObject>()));
                    case "Level0ValueFor":
                        return new Level0ValueFor(
                            new Level0ValueForFactory().GetNew(item["_iValueFor"].Value<JObject>()));
                    default:
                        throw new InvalidOperationException();
                }
            }
            var value = serializer.Deserialize(reader, typeof(Level0ValueFor));
            
            return value;
        }

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(Level0ValueFor));
        }
    }
}