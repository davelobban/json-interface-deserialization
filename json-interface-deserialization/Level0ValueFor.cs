using System;
using Newtonsoft.Json;

namespace json_interface_deserialization
{
    public class Level0ValueFor : IValueFor, IEquatable<Level0ValueFor>
    {
        public string Typ = "Level0ValueFor";
        [JsonProperty]
        private IValueFor _iValueFor;

        public string ValueFor(Env environment) => $"L0: {_iValueFor.ValueFor(environment)}";
        [JsonConstructor]
        public Level0ValueFor()
        {
        }
        public Level0ValueFor(IValueFor iValueFor)
        {
            _iValueFor = iValueFor;
        }

        public bool Equals(Level0ValueFor other)
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
            return Equals((Level0ValueFor) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Typ != null ? Typ.GetHashCode() : 0) * 397) ^ (_iValueFor != null ? _iValueFor.GetHashCode() : 0);
            }
        }
    }
}