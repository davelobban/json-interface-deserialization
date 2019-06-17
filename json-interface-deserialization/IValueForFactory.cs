using System;
using Newtonsoft.Json.Linq;

namespace json_interface_deserialization
{
    public abstract class IValueForFactory<T> where T : IValueFor
    {
        public T GetNew(JObject item)
        {
            var childTypeName = item["_iValueFor"].Value<JObject>()["Typ"].Value<string>();
            if (childTypeName == "EnvironmentPrinter")
                return ConstructInstance(new EnvironmentPrinter(), item);
            if (childTypeName == "HappyEnvironmentPrinter")
                return ConstructInstance(new HappyEnvironmentPrinter(), item);
            if (childTypeName == "Level0ValueFor")
                return ConstructInstance(new Level0ValueForFactory().GetNew(item["_iValueFor"].Value<JObject>()), item);
            if (childTypeName == "Level1ValueFor")
                return ConstructInstance(new Level1ValueForFactory().GetNew(item["_iValueFor"].Value<JObject>()), item);
            if (childTypeName == "SeedableValueFor")
                return ConstructInstance(new SeedableValueForFactory().GetNew(item["_iValueFor"].Value<JObject>()), item);
            throw new InvalidOperationException();
        }

        protected abstract T ConstructInstance(IValueFor value, JObject item);
    }
}