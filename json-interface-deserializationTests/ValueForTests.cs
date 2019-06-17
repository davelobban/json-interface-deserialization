using json_interface_deserialization;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Tests
{
    public class ValueForTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ThreeLevelsNested_PrintsExpectedValue()
        {
            var printer = new EnvironmentPrinter();
            var level1 = new Level1ValueFor(printer);
            var level0 = new Level0ValueFor(level1);
            Assert.AreEqual("This is Preprod", level0.ValueFor(Env.Preprod));
        }

        [Test]
        public void SerialiseEnvironmentPrinter_EqualsUnserialisedValue()
        {
            var printer = new EnvironmentPrinter();
            
            var serialised = JsonConvert.SerializeObject(printer);
            var deserialised = JsonConvert.DeserializeObject<EnvironmentPrinter>(serialised);

            Assert.AreEqual(printer, deserialised);
        }

        [Test]
        public void SerialiseOneLevelsNested_EqualsUnserialisedValue()
        {
            var printer = new EnvironmentPrinter();
            var level1 = new Level1ValueFor(printer);

            //var settings = new JsonSerializerSettings()
            //{
            //    TypeNameHandling = TypeNameHandling.Auto
            //};
            var serialised = JsonConvert.SerializeObject(level1, Formatting.Indented,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Serialize
                });
            var deserialised = JsonConvert.DeserializeObject<Level1ValueFor>(serialised, new IValueForConverter());

            Assert.AreEqual(level1, deserialised);
            Assert.AreEqual("This is Preprod", level1.ValueFor(Env.Preprod));
        }

        //[Test]
        //public void SerialiseThreeLevelsNested_EqualsUnserialisedValue()
        //{
        //    var printer = new EnvironmentPrinter();
        //    var level1 = new Level1ValueFor(printer);
        //    var level0 = new Level0ValueFor(level1);

        //    var serialised=JsonConvert.SerializeObject(level0);
        //    var deserialised = JsonConvert.DeserializeObject<Level0ValueFor>(serialised);

        //    Assert.AreEqual(level0, deserialised);
        //}
    }


}