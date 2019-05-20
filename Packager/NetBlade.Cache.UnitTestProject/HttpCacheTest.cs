using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NetBlade.Cache.UnitTestProject
{
    [TestClass]
    public class HttpCacheTest
    {
        [TestMethod]
        public void HttpCache_Get_Test()
        {
            DateTime d1 = HttpCache.Default.Get<DateTime>("dateTime", () => { return DateTime.Now; }, false);
            Assert.AreEqual(d1, HttpCache.Default.Get<DateTime>("dateTime"));

            DateTime d2 = DateTime.Now.AddDays(1);
            HttpCache.Default.Add<DateTime>("dateTime", d2);
            Assert.AreEqual(d2, HttpCache.Default.Get<DateTime>("dateTime"));
        }
    }
}
