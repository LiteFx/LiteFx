using LiteFx.DomainResult;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LiteFx.Specs.DomainResultUnitTests
{
	[TestClass]
	public class ForbiddenResultTest
	{
		[TestMethod]
		public void DoisForbiddenResultIguaisDevemRetornarEqualIgualATrue()
		{
			ForbiddenResult resultDest = new ForbiddenResult("message");
			ForbiddenResult resultSource = new ForbiddenResult("message");

			Assert.IsTrue(resultDest.Equals(resultSource));
		}

		[TestMethod]
		public void DoisForbiddenResultDiferentesDevemRetornarEqualIgualAFalse()
		{
			ForbiddenResult resultDest = new ForbiddenResult("message");
			ForbiddenResult resultSource = new ForbiddenResult("other message");

			Assert.IsFalse(resultDest.Equals(resultSource));
		}
	}
}