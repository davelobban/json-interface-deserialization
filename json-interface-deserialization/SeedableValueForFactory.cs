using System;
using Newtonsoft.Json.Linq;

namespace json_interface_deserialization
{
    public class SeedableValueForFactory
    {
        public SeedableValueFor GetNew(JObject item)
        {
            var seed = item["_seed"].Value<string>();
            if (item["_iValueFor"].Value<JObject>()["Typ"].Value<string>() == "EnvironmentPrinter")
            {
                return new SeedableValueFor(new EnvironmentPrinter(), seed);
            }

            if (item["_iValueFor"].Value<JObject>()["Typ"].Value<string>() == "HappyEnvironmentPrinter")
                return new SeedableValueFor(new HappyEnvironmentPrinter(), seed);

            if (item["_iValueFor"].Value<JObject>()["Typ"].Value<string>() == "Level1ValueFor")
                return new SeedableValueFor(new Level1ValueForFactory().GetNew(item["_iValueFor"].Value<JObject>()), seed);
            if (item["_iValueFor"].Value<JObject>()["Typ"].Value<string>() == "Level0ValueFor")
                return new SeedableValueFor(new Level0ValueForFactory().GetNew(item["_iValueFor"].Value<JObject>()), seed);
            throw new InvalidOperationException();
        }
    }
}