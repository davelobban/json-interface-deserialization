using System;
using Newtonsoft.Json.Linq;

namespace json_interface_deserialization
{
    public class SeedableValueForFactory : IValueForFactory<SeedableValueFor>
    {
        protected override SeedableValueFor ConstructInstance(IValueFor value, JObject item)
        {
            var seed = item["_seed"].Value<string>();
            return new SeedableValueFor(value, seed);
        }
    }
}