using System;
using Newtonsoft.Json;

namespace json_interface_deserialization
{
    public class SeedableValueFor : IValueFor, IEquatable<SeedableValueFor>
    {
        [JsonProperty] public string Typ = "SeedableValueFor";
        [JsonProperty] private IValueFor _iValueFor;

        [JsonProperty] private string _seed;
        public string ValueFor(Env environment) => $"[{_seed}].. {_iValueFor.ValueFor(environment)}";

        [JsonConstructor]
        public SeedableValueFor()
        {
        }

        public SeedableValueFor(IValueFor iValueFor, string seed)
        {
            _iValueFor = iValueFor;
            _seed = seed;
        }

        public bool Equals(SeedableValueFor other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Typ, other.Typ) && Equals(_iValueFor, other._iValueFor) && string.Equals(_seed, other._seed);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((SeedableValueFor) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Typ != null ? Typ.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (_iValueFor != null ? _iValueFor.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (_seed != null ? _seed.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}