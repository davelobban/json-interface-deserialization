using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace json_interface_deserialization
{
    public enum Env
    {
        Preprod,
        Production
    }

    public interface IValueFor
    {
        string ValueFor(Env environment);
    }

    public class Level0ValueFor : IValueFor//, IEquatable<Level0ValueFor>
    {
        public string Typ = "Level0ValueFor";
        [JsonProperty]
        private IValueFor _iValueFor;

        public string ValueFor(Env environment) => _iValueFor.ValueFor(environment);
        [JsonConstructor]
        public Level0ValueFor()
        {
        }
        public Level0ValueFor(IValueFor iValueFor)
        {
            _iValueFor = iValueFor;
        }

    }

    public class Level1ValueFor : IValueFor, IEquatable<Level1ValueFor>
    {
        [JsonProperty]
        public string Typ = "Level1ValueFor";
        [JsonProperty]
        private IValueFor _iValueFor;
        public string ValueFor(Env environment) => _iValueFor.ValueFor(environment);

        [JsonConstructor]
        public Level1ValueFor()
        {
        }

        public Level1ValueFor(IValueFor iValueFor)
        {
            _iValueFor = iValueFor;
        }

        public bool Equals(Level1ValueFor other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Typ, other.Typ) && Equals(_iValueFor, other._iValueFor);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Level1ValueFor) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Typ != null ? Typ.GetHashCode() : 0) * 397) ^ (_iValueFor != null ? _iValueFor.GetHashCode() : 0);
            }
        }
    }
    public class IValueForConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value, typeof(Level1ValueFor));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var item = JObject.Load(reader);
            var value = serializer.Deserialize(reader, typeof(EnvironmentPrinter));  //This is the ultimate payload in one test but it doesn't cover other scenario implementations and it omits any intermediate 'IValueFor's.
            return value;
        }

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(IValueFor));
        }
    }
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
                if (item["_iValueFor"].Value<JObject>()["Typ"].Value<string>()=="EnvironmentPrinter")
                return new Level1ValueFor(new EnvironmentPrinter());
                if (item["_iValueFor"].Value<JObject>()["Typ"].Value<string>() == "HappyEnvironmentPrinter")
                    return new Level1ValueFor(new HappyEnvironmentPrinter());
            }
            var value = serializer.Deserialize(reader, typeof(Level1ValueFor));  //This is the ultimate payload in one test but it doesn't cover other scenario implementations and it omits any intermediate 'IValueFor's.
            
            return value;
        }

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(Level1ValueFor));
        }
    }

    public class EnvironmentPrinter : IValueFor, IEquatable<EnvironmentPrinter>
    {
        public virtual string Typ => "EnvironmentPrinter";

        public virtual string ValueFor(Env environment) => $"This is {environment}";


        public bool Equals(EnvironmentPrinter other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Typ, other.Typ);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((EnvironmentPrinter) obj);
        }

        public override int GetHashCode()
        {
            return (Typ != null ? Typ.GetHashCode() : 0);
        }
    }
    public class HappyEnvironmentPrinter : EnvironmentPrinter
    {
        public override string Typ => "HappyEnvironmentPrinter";

        public override string ValueFor(Env environment) => $"Happy to  say, This is {environment}";

        
    }
}
