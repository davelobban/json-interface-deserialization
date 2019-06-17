namespace json_interface_deserialization
{
    public class Level1ValueForConverter : IValueForConverter<Level1ValueFor>
    {
        protected override IValueFor ConstructedInstance(IValueFor value)
        {
            return new Level1ValueFor(value);
        }
    }
}