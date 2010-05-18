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


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for Encrypt
        ///</summary>
        [TestMethod()]
        public void EncryptTest()
        {
            string textToEncrypt = "Text to Encrypt";
            string expected = "/5lqlyBzI8BOlbH0g4b2qg==";

            string actual;
            actual = CryptographyUtil.Encrypt(textToEncrypt);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Decrypt
        ///</summary>
        [TestMethod()]
        public void DecryptTest()
        {
            string textToDecrypt = "/5lqlyBzI8BOlbH0g4b2qg==";
            string expected = "Text to Encrypt";
            string actual;
            actual = CryptographyUtil.Decrypt(textToDecrypt);
            Assert.AreEqual(expected, actual);
        }

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

        /// <summary>
        ///Cria um sal, o criptografa e o descriptografa.
        ///</summary>
        [TestMethod()]
        public void CreateSaltEncryptedAndDecryptTest()
        {
            string salt = CryptographyUtil.CreateSalt(10);
            string encryptedSalt = CryptographyUtil.Encrypt(salt);
            string decryptedSalt = CryptographyUtil.Decrypt(encryptedSalt);

            Assert.AreEqual(salt, decryptedSalt);
        }
    }
}
