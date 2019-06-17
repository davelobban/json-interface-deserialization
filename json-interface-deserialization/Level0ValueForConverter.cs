using System.Collections.Generic;

namespace json_interface_deserialization
{
    public class Level0ValueForConverter : IValueForConverter<Level0ValueFor>
    {
        protected override IValueFor ConstructedInstance(IValueFor value, Dictionary<string, object> parms)
        {
            return new Level0ValueFor(value);
        }
    }
}