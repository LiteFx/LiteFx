using LiteFx.DomainResult;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LiteFx.Specs.DomainResultUnitTests
{
	[TestClass]
	public class OkResultTest
	{
		[TestMethod]
		public void DoisOkResultIguaisDevemRetornarEqualIgualATrue()
		{
			OkResult resultDest = new OkResult();
			OkResult resultSource = new OkResult();

			Assert.IsTrue(resultDest.Equals(resultSource));
		}

		[TestMethod]
		public void DoisOkResultDiferentesDevemRetornarEqualIgualAFalse()
		{
			OkResult<string> resultDest = new OkResult<string>();
			OkResult<int> resultSource = new OkResult<int>();

			Assert.IsFalse(resultDest.Equals(resultSource));
		}
	}
}