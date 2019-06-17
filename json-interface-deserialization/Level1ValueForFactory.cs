using Newtonsoft.Json.Linq;

namespace json_interface_deserialization
{
    public class Level1ValueForFactory : IValueForFactory<Level1ValueFor>
    {
        protected override Level1ValueFor ConstructInstance(IValueFor value, JObject item)
        {
            return new Level1ValueFor(value);
        }
    }
}