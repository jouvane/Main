using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NetBlade.Mask.UnitTestProject
{
    [TestClass]
    public class MaskTest
    {
        [TestMethod]
        public void CleanValue_CNPJ_Test()
        {
            MaskAttribute mask = new MaskAttribute("99.999.999/9999-99");
            Assert.AreEqual("68837346000182", mask.CleanValue("68.837.346/0001-82"));
        }

        [TestMethod]
        public void CleanValue_CPF_Test()
        {
            MaskAttribute mask = new MaskAttribute("999.999.999-99");
            Assert.AreEqual("07992474643", mask.CleanValue("079.924.746-43"));
        }

        [TestMethod]
        public void CleanValue_MultMask_CNPJ_Test()
        {
            MaskAttribute mask = new MaskAttribute(new string[] { "99.999.999/9999-99", "999.999.999-99" });
            Assert.AreEqual("68837346000182", mask.CleanValue("68.837.346/0001-82"));
        }

        [TestMethod]
        public void CleanValue_MultMask_CPF_Test()
        {
            MaskAttribute mask = new MaskAttribute(new string[] { "99.999.999/9999-99", "999.999.999-99" });
            Assert.AreEqual("07992474643", mask.CleanValue("079.924.746-43"));
        }

        [TestMethod]
        public void CleanValue_MultMask_Telefone_8_Dig_Test()
        {
            MaskAttribute mask = new MaskAttribute(new string[] { "(99) 9999-9999", "(31) 99999-9999" });
            Assert.AreEqual("3187423236", mask.CleanValue("(31) 8742-3236"));
        }

        [TestMethod]
        public void CleanValue_MultMask_Telefone_9_Dig_Test()
        {
            MaskAttribute mask = new MaskAttribute(new string[] { "(99) 9999-9999", "(31) 99999-9999" });
            Assert.AreEqual("31987423236", mask.CleanValue("(31) 98742-3236"));
        }

        [TestMethod]
        public void Format_CNPJ_Test()
        {
            MaskAttribute mask = new MaskAttribute("99.999.999/9999-99");
            Assert.AreEqual("68.837.346/0001-82", mask.Format("68837346000182"));
        }

        [TestMethod]
        public void Format_CPF_Test()
        {
            MaskAttribute mask = new MaskAttribute("999.999.999-99");
            Assert.AreEqual("079.924.746-43", mask.Format("07992474643"));
        }

        [TestMethod]
        public void Format_End_Test()
        {
            MaskAttribute mask = new MaskAttribute("99.99+++++");
            Assert.AreEqual("12.34+++++", mask.Format("1234"));
        }

        [TestMethod]
        public void Format_INT_Test()
        {
            MaskAttribute mask = new MaskAttribute("999");
            Assert.AreEqual("1", mask.Format("1"));
        }

        [TestMethod]
        public void Format_Lat_Long_Test()
        {
            MaskAttribute mask = new MaskAttribute("+00°00'00.000\"");
            Assert.AreEqual("+60°58'35.100\"", mask.Format("605835100"));
        }

        [TestMethod]
        public void Format_MultMask_CNPJ_Test()
        {
            MaskAttribute mask = new MaskAttribute(new string[] { "99.999.999/9999-99", "999.999.999-99" });
            Assert.AreEqual("68.837.346/0001-82", mask.Format("68837346000182"));
        }

        [TestMethod]
        public void Format_MultMask_CPF_Test()
        {
            MaskAttribute mask = new MaskAttribute(new string[] { "99.999.999/9999-99", "999.999.999-99" });
            Assert.AreEqual("079.924.746-43", mask.Format("07992474643"));
        }

        [TestMethod]
        public void Format_MultMask_Telefone_8_Dig_Test()
        {
            MaskAttribute mask = new MaskAttribute(new string[] { "(99) 9999-9999", "(99) 99999-9999" });
            Assert.AreEqual("(31) 8742-3236", mask.Format("3187423236"));
        }

        [TestMethod]
        public void Format_MultMask_Telefone_9_Dig_Test()
        {
            MaskAttribute mask = new MaskAttribute(new string[] { "(99) 9999-9999", "(99) 99999-9999" });
            Assert.AreEqual("(31) 98742-3236", mask.Format("31987423236"));
        }
    }
}
