using System.Collections.Generic;

namespace json_interface_deserialization
{
    // ReSharper disable once IdentifierTypo
    public class SeedableValueForConverter : IValueForConverter<SeedableValueFor>
    {
        protected override IValueFor ConstructedInstance(IValueFor value, Dictionary<string, object> parms)
        {
            var seed = parms["seed"].ToString();
            return new SeedableValueFor(value, seed);
        }
    }
}
