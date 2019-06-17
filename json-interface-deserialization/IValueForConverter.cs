using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace json_interface_deserialization
{
    public abstract class IValueForConverter<T>: JsonConverter where T : IValueFor
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value, typeof(T));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var item = JObject.Load(reader);
            var genericTypeName = typeof(T).UnderlyingSystemType.Name;
            if (item["Typ"].Value<string>() == genericTypeName)
            {
                var typeName = item["_iValueFor"].Value<JObject>()["Typ"].Value<string>();
                IValueFor constructed;
                var parms= new Dictionary<string, object>();
                switch (typeName)
                {
                    case "EnvironmentPrinter":
                    {
                        constructed = new EnvironmentPrinter();
                        break;
                    }
                    case "HappyEnvironmentPrinter":
                    {
                        constructed = new HappyEnvironmentPrinter();
                        break;
                    }
                    case "Level1ValueFor":
                    {
                        //TODO: can tidy these
                        constructed =
                            new Level1ValueForFactory().GetNew(item["_iValueFor"].Value<JObject>());
                        break;
                    }
                    case "Level0ValueFor":
                    {
                        constructed =
                            new Level0ValueForFactory().GetNew(item["_iValueFor"].Value<JObject>());
                        break;
                    }
                    case "SeedableValueFor":
                    {
                        constructed =
                            new SeedableValueForFactory().GetNew(item["_iValueFor"].Value<JObject>());
                        break;
                    }
                    default:
                        throw new InvalidOperationException($"Could not find a factory for type name {typeName} in converter");
                }
                //TODO: This is too specific
                if (item["_seed"] != null)
                {
                    parms.Add("seed", item["_seed"].Value<string>());
                }
                else
                {
                    if (item["_iValueFor"].Value<JObject>()["_seed"] != null)
                    {
                        parms.Add("seed", item["_iValueFor"].Value<JObject>()["_seed"].Value<string>());
                    }
                }

                return ConstructedInstance(constructed, parms);
            }

            throw new InvalidOperationException($"Typ was not a property found in converter");
        }

        protected abstract IValueFor ConstructedInstance(IValueFor value, Dictionary<string, object> parms);

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(T));
        }
    }
}