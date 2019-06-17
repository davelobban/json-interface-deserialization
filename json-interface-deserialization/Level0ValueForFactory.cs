using System;
using Newtonsoft.Json.Linq;

namespace json_interface_deserialization
{
    public class Level0ValueForFactory
    {
        public Level0ValueFor GetNew(JObject item)
        {
            if (item["_iValueFor"].Value<JObject>()["Typ"].Value<string>() == "EnvironmentPrinter")
                return new Level0ValueFor(new EnvironmentPrinter());
            if (item["_iValueFor"].Value<JObject>()["Typ"].Value<string>() == "HappyEnvironmentPrinter")
                return new Level0ValueFor(new HappyEnvironmentPrinter());

            if (item["_iValueFor"].Value<JObject>()["Typ"].Value<string>() == "Level1ValueFor")
                return new Level0ValueFor(new Level1ValueForFactory().GetNew(item["_iValueFor"].Value<JObject>()));
            throw new InvalidOperationException();
        }
    }
}