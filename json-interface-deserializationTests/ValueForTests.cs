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

        //[Test]
        //public void ThreeLevelsNested_PrintsExpectedValue()
        //{
        //    var printer = new EnvironmentPrinter();
        //    var level1 = new Level1ValueFor(printer);
        //    var level0 = new Level0ValueFor(level1);
        //    Assert.AreEqual("This is Preprod", level0.ValueFor(Env.Preprod));
        //}

        //[Test]
        //public void SerialiseEnvironmentPrinter_EqualsUnserialisedValue()
        //{
        //    var printer = new EnvironmentPrinter();
            
        //    var serialised = JsonConvert.SerializeObject(printer);
        //    var deserialised = JsonConvert.DeserializeObject<EnvironmentPrinter>(serialised);

        //    Assert.AreEqual(printer, deserialised);
        //}

        [Test]
        public void SerialiseOneLevelsNested_ReturnsExpectedValue()
        {
            var printer = new EnvironmentPrinter();
            var level1 = new Level1ValueFor(printer);
            var serialised = Serialise(level1);
            var deserialised = JsonConvert.DeserializeObject<Level1ValueFor>(serialised, new Level1ValueForConverter());

            Assert.AreEqual(level1, deserialised);
            Assert.AreEqual("This is Preprod", deserialised.ValueFor(Env.Preprod));
        }

        [Test]
        public void HappySerialiseOneLevelsNested_ReturnsExpectedValue()
        {
            var printer = new HappyEnvironmentPrinter();
            var level1 = new Level1ValueFor(printer);
            var serialised = Serialise(level1);
            var deserialised = JsonConvert.DeserializeObject<Level1ValueFor>(serialised, new Level1ValueForConverter());

            Assert.AreEqual(level1, deserialised);
            Assert.AreEqual("Happy to  say, This is Preprod", deserialised.ValueFor(Env.Preprod));
        }

        private static string Serialise(IValueFor level1)
        {
            var serializeObject = JsonConvert.SerializeObject(level1, Formatting.Indented,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Serialize
                });
            var serialised = serializeObject;
            return serialised;
        }

        /*
        [Test]
        public void SerialiseTwoLevelsNested_EqualsUnserialisedValue()
        {
            var printer = new EnvironmentPrinter();
            var level1 = new Level1ValueFor(printer);
            var level0 = new Level0ValueFor(level1);

            var serialised = JsonConvert.SerializeObject(level0, Formatting.Indented,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Serialize
                });
            var deserialised = JsonConvert.DeserializeObject<Level0ValueFor>(serialised, new IValueForConverter());

            //Assert.AreEqual(level0, deserialised);
            Assert.AreEqual("This is Preprod", deserialised.ValueFor(Env.Preprod));
        }


        [Test]
        public void SerialiseFourLevelsNested_EqualsUnserialisedValue()
        {
            var printer = new EnvironmentPrinter();
            var level1 = new Level1ValueFor(printer);
            var level0 = new Level0ValueFor(level1);
            var level00 = new Level0ValueFor(level0);
            var level000 = new Level0ValueFor(level00);

            var serialised = JsonConvert.SerializeObject(level000, Formatting.Indented,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Serialize
                });
            var deserialised = JsonConvert.DeserializeObject<Level0ValueFor>(serialised, new IValueForConverter());

            //Assert.AreEqual(level0, deserialised);
            Assert.AreEqual("This is Preprod", deserialised.ValueFor(Env.Preprod));
        }


        [Test]
        public void HappySerialiseFourLevelsNested_EqualsUnserialisedValue()
        {
            var printer = new HappyEnvironmentPrinter();
            var level1 = new Level1ValueFor(printer);
            var level0 = new Level0ValueFor(level1);
            var level00 = new Level0ValueFor(level0);
            var level000 = new Level0ValueFor(level00);

            var serialised = JsonConvert.SerializeObject(level000, Formatting.Indented,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Serialize
                });
            var deserialised = JsonConvert.DeserializeObject<Level0ValueFor>(serialised, new IValueForConverter());

            Assert.AreEqual("Happy to  say, This is Preprod", deserialised.ValueFor(Env.Preprod));
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
        */
    }


}