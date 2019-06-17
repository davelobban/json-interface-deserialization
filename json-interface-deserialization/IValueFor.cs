using System;

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
