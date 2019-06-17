using System;
using Newtonsoft.Json.Linq;

namespace json_interface_deserialization
{
    public class Level1ValueForFactory
    {
        public Level1ValueFor GetNew(JObject item)
        {
            if (item["_iValueFor"].Value<JObject>()["Typ"].Value<string>() == "EnvironmentPrinter")
                return new Level1ValueFor(new EnvironmentPrinter());
            if (item["_iValueFor"].Value<JObject>()["Typ"].Value<string>() == "HappyEnvironmentPrinter")
                return new Level1ValueFor(new HappyEnvironmentPrinter());
            if (item["_iValueFor"].Value<JObject>()["Typ"].Value<string>() == "Level0ValueFor")
                return new Level1ValueFor(new Level0ValueForFactory().GetNew(item["_iValueFor"].Value<JObject>()));
            throw new InvalidOperationException();
        }
    }
}