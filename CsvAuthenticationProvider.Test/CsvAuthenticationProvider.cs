using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CsvAuthenticationProvider.Test
{
    [TestClass]
    public class CsvAuthenticationProviderTest
    {
        [TestMethod]
        public void Check_If_User_Exists()
        {
            CsvAuthenticationProvider provider = new CsvAuthenticationProvider();
            var expected = true;
            var actual = provider.UserExists("User");

            Assert.AreEqual(expected,actual);
        }
    }
}
