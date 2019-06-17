namespace json_interface_deserialization
{
    public class Level0ValueForConverter : IValueForConverter<Level0ValueFor>
    {
        protected override IValueFor ConstructedInstance(IValueFor value)
        {
            return new Level0ValueFor(value);
        }
    }
}