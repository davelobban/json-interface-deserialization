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
