using Newtonsoft.Json.Linq;

namespace json_interface_deserialization
{
    public class Level0ValueForFactory : IValueForFactory<Level0ValueFor>
    {
        protected override Level0ValueFor ConstructInstance(IValueFor value, JObject item)
        {
            return new Level0ValueFor(value);
        }
    }

    
}