using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Elk.Tests
{
    [TestClass]
    public class ConfigurationTests
    {
        [TestMethod]
        public void Test()
        {
            Config.LoadConfiguration();
            Assert.AreEqual(2, Config.SourceOptions.Count);

            EventLogSourceOption first = Config.SourceOptions.First();
            Assert.AreEqual("test", first.Name);
            Assert.AreEqual("localhost", first.Host);
            Assert.AreEqual("Application", first.LogName);
            Assert.AreEqual("IIS Express", first.ProviderName);
        }


    }
}
