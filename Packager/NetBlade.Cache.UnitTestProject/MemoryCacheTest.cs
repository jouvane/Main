using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NetBlade.Cache.UnitTestProject
{
    [TestClass]
    public class MemoryCacheTest
    {
        [TestMethod]
        public void MemoryCache_Get_Test()
        {
            DateTime d1 = MemoryCache.Default.Get<DateTime>("dateTime", () => { return DateTime.Now; }, false);
            Assert.AreEqual(d1, MemoryCache.Default.Get<DateTime>("dateTime"));

            DateTime d2 = DateTime.Now.AddDays(1);
            MemoryCache.Default.Add<DateTime>("dateTime", d2);
            Assert.AreEqual(d2, MemoryCache.Default.Get<DateTime>("dateTime"));
        }
    }
}
