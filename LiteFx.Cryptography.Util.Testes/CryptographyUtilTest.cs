using LiteFx.Cryptography.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace LiteFx.Cryptography.Util.Tests
{
    
    
    /// <summary>
    ///This is a test class for CryptographyUtilTest and is intended
    ///to contain all CryptographyUtilTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CryptographyUtilTest
    {
        /// <summary>
        ///A test for CreateSalt
        ///</summary>
        [TestMethod()]
        public void CreateSaltTest()
        {
            int length = 10;
            string actual;
            actual = CryptographyUtil.CreateSalt(length);
            Assert.AreEqual(16, actual.Length);
        }
    }
}
