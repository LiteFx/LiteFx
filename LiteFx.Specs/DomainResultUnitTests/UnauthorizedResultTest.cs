using LiteFx.DomainResult;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LiteFx.Specs.DomainResultUnitTests
{
	[TestClass]
	public class UnauthorizedResultTest
	{
		[TestMethod]
		public void DoisUnauthorizedResultIguaisDevemRetornarEqualIgualATrue()
		{
			UnauthorizedResult resultDest = new UnauthorizedResult("message");
			UnauthorizedResult resultSource = new UnauthorizedResult("message");

			Assert.IsTrue(resultDest.Equals(resultSource));
		}

		[TestMethod]
		public void DoisUnauthorizedResultDiferentesDevemRetornarEqualIgualAFalse()
		{
			UnauthorizedResult resultDest = new UnauthorizedResult("message");
			UnauthorizedResult resultSource = new UnauthorizedResult("other message");

			Assert.IsFalse(resultDest.Equals(resultSource));
		}
	}
}