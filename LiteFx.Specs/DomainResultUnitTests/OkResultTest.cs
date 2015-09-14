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

		[TestMethod]
		public void DoisOkResultIntIguaisDevemRetornarEqualIgualATrue()
		{
			OkResult<int> resultDest = new OkResult<int>(1);
			OkResult<int> resultSource = new OkResult<int>(1);

			Assert.IsTrue(resultDest.Equals(resultSource));
		}

		[TestMethod]
		public void DoisOkResultIntDiferentesDevemRetornarEqualIgualAFalse()
		{
			OkResult<int> resultDest = new OkResult<int>(0);
			OkResult<int> resultSource = new OkResult<int>(1);

			Assert.IsFalse(resultDest.Equals(resultSource));
		}
	}
}