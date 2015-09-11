using LiteFx.DomainResult;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LiteFx.Specs.DomainResultUnitTests
{
	[TestClass]
	public class ErrorResultTest
	{
		[TestMethod]
		public void DoisErrorsResultIguaisDevemRetornarEqualIgualATrue()
		{
			ErrorResult resultDest = new ErrorResult("key", "message");
			ErrorResult resultSource = new ErrorResult("key", "message");

			Assert.IsTrue(resultDest.Equals(resultSource));
		}

		[TestMethod]
		public void DoisErrorsResultDiferentesDevemRetornarEqualIgualAFalse()
		{
			ErrorResult resultDest = new ErrorResult("key", "message");
			ErrorResult resultSource = new ErrorResult("key", "other message");

			Assert.IsFalse(resultDest.Equals(resultSource));
		}

		[TestMethod]
		public void DoisErrorsResultGenericsIguaisDevemRetornarEqualIgualATrue()
		{
			ErrorResult<string> resultDest = new ErrorResult<string>("key", "message");
			ErrorResult<string> resultSource = new ErrorResult<string>("key", "message");

			Assert.IsTrue(resultDest.Equals(resultSource));
		}

		[TestMethod]
		public void DoisErrorsResultGenericsDiferentesDevemRetornarEqualIgualAFalse()
		{
			ErrorResult<string> resultDest = new ErrorResult<string>("key", "message");
			ErrorResult<int> resultSource = new ErrorResult<int>(0, "other message");

			Assert.IsFalse(resultDest.Equals(resultSource));
		}
	}
}