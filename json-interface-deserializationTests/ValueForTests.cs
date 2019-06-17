using json_interface_deserialization;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Tests
{
    public class ValueForTests
    {
        private readonly string _happyToSay = "Happy to  say, ";

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
            Assert.AreEqual("L1.. This is Preprod", deserialised.ValueFor(Env.Preprod));
        }

        [Test]
        public void HappySerialiseOneLevelsNested_ReturnsExpectedValue()
        {
            var printer = new HappyEnvironmentPrinter();
            var level1 = new Level1ValueFor(printer);
            var serialised = Serialise(level1);
            var deserialised = JsonConvert.DeserializeObject<Level1ValueFor>(serialised, new Level1ValueForConverter());

            Assert.AreEqual(level1, deserialised);
            Assert.AreEqual($"L1.. {_happyToSay}This is Preprod", deserialised.ValueFor(Env.Preprod));
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
        
        
        [Test]
        public void SerialiseTwoLevelsNested_EqualsUnserialisedValue()
        {
            var printer = new EnvironmentPrinter();
            var level1 = new Level1ValueFor(printer);
            var level0 = new Level0ValueFor(level1);

            var serialised = Serialise(level0);
            var deserialised = JsonConvert.DeserializeObject<Level0ValueFor>(serialised, new Level0ValueForConverter());

            Assert.AreEqual(level0, deserialised);
            Assert.AreEqual($"L0: L1.. This is Preprod", deserialised.ValueFor(Env.Preprod));
        }
        [Test]
        public void HappySerialiseTwoLevelsNested_EqualsUnserialisedValue()
        {
            var printer = new HappyEnvironmentPrinter();
            var level1 = new Level1ValueFor(printer);
            var level0 = new Level0ValueFor(level1);

            var serialised = Serialise(level0);
            var deserialised = JsonConvert.DeserializeObject<Level0ValueFor>(serialised, new Level0ValueForConverter());

            Assert.AreEqual(level0, deserialised);
            Assert.AreEqual($"L0: L1.. {_happyToSay}This is Preprod", deserialised.ValueFor(Env.Preprod));
        }
        [Test]
        public void SerialiseThreeLevelsNested_EqualsUnserialisedValue()
        {
            var printer = new EnvironmentPrinter();
            var level1 = new Level1ValueFor(printer);
            var level0 = new Level0ValueFor(level1);
            var level00 = new Level0ValueFor(level0);

            var serialised = Serialise(level00);
            var deserialised = JsonConvert.DeserializeObject<Level0ValueFor>(serialised, new Level0ValueForConverter());

            Assert.AreEqual(level00, deserialised);
            Assert.AreEqual($"L0: L0: L1.. This is Preprod", deserialised.ValueFor(Env.Preprod));
        }
        [Test]
        public void HappySerialiseThreeLevelsNested_EqualsUnserialisedValue()
        {
            var printer = new HappyEnvironmentPrinter();
            var level1 = new Level1ValueFor(printer);
            var level0 = new Level0ValueFor(level1);
            var level00 = new Level0ValueFor(level0);

            var serialised = Serialise(level00);
            var deserialised = JsonConvert.DeserializeObject<Level0ValueFor>(serialised, new Level0ValueForConverter());

            Assert.AreEqual(level00, deserialised);
            Assert.AreEqual($"L0: L0: L1.. {_happyToSay}This is Preprod", deserialised.ValueFor(Env.Preprod));
        }
        [Test]
        public void SerialiseFourLevelsNested_EqualsUnserialisedValue()
        {
            var printer = new EnvironmentPrinter();
            var level0wrappingPrinter = new Level0ValueFor(printer);
            var level1 = new Level1ValueFor(level0wrappingPrinter);
            var level0 = new Level0ValueFor(level1);
            var level00 = new Level0ValueFor(level0);

            var serialised = Serialise(level00);
            var deserialised = JsonConvert.DeserializeObject<Level0ValueFor>(serialised, new Level0ValueForConverter());

            Assert.AreEqual(level00, deserialised);
            Assert.AreEqual($"L0: L0: L1.. L0: This is Preprod", deserialised.ValueFor(Env.Preprod));
        }
        [Test]
        public void HappySerialiseFourLevelsNested_EqualsUnserialisedValue()
        {
            var printer = new HappyEnvironmentPrinter();
            var level0wrappingPrinter = new Level0ValueFor(printer);
            var level1 = new Level1ValueFor(level0wrappingPrinter);
            var level0 = new Level0ValueFor(level1);
            var level00 = new Level0ValueFor(level0);

            var serialised = Serialise(level00);
            var deserialised = JsonConvert.DeserializeObject<Level0ValueFor>(serialised, new Level0ValueForConverter());

            Assert.AreEqual(level00, deserialised);
            Assert.AreEqual($"L0: L0: L1.. L0: {_happyToSay}This is Preprod", deserialised.ValueFor(Env.Preprod));
        }

        [Test]
        public void HappySerialiseFiveLevelsNestedAroundL0_EqualsUnserialisedValue()
        {
            var printer = new HappyEnvironmentPrinter();
            var level0wrappingPrinter = new Level0ValueFor(printer);
            var level1 = new Level1ValueFor(level0wrappingPrinter);
            var level0 = new Level0ValueFor(level1);
            var seed = "theseedvalue";
            var seedableLevel = new SeedableValueFor(level0, $"{seed}");
            var level00 = new Level0ValueFor(seedableLevel);

            var serialised = Serialise(level00);
            var deserialised = JsonConvert.DeserializeObject<Level0ValueFor>(serialised, new Level0ValueForConverter());

            Assert.AreEqual(level00, deserialised);
            Assert.AreEqual($"L0: [{seed}].. L0: L1.. L0: {_happyToSay}This is Preprod", deserialised.ValueFor(Env.Preprod));
        }


        [Test]
        public void HappySerialiseFiveLevelsNestedAroundL1_EqualsUnserialisedValue()
        {
            var printer = new HappyEnvironmentPrinter();
            var level1wrappingPrinter = new Level1ValueFor(printer);
            var level1 = new Level1ValueFor(level1wrappingPrinter);
            var level0 = new Level0ValueFor(level1);
            var seed = "theseedvalue";
            var seedableLevel = new SeedableValueFor(level0, $"{seed}");
            var level11 = new Level1ValueFor(seedableLevel);

            var serialised = Serialise(level11);
            var deserialised = JsonConvert.DeserializeObject<Level1ValueFor>(serialised, new Level1ValueForConverter());

            Assert.AreEqual(level11, deserialised);
            Assert.AreEqual($"L1.. [{seed}].. L0: L1.. L1.. {_happyToSay}This is Preprod", deserialised.ValueFor(Env.Preprod));
        }
    }
}