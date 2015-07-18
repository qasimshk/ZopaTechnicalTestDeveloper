// ReSharper disable InconsistentNaming
namespace Zopa.Calculations.Console.UnitTest
{
	using System;

	using NUnit.Framework;

	[TestFixture]
	public class SystemConsoleCalculationsOutputUnitTests
	{
		[Test, Category("OutputQuoteCalculationResult")]
		[ExpectedException(typeof(ArgumentNullException))]
		public void OutputQuoteCalculationResult_WhenResultIsNull_ThrowArgumentNullException()
		{
			var output = new SystemConsoleCalculationsOutput() as ICalculationsOutput;

			output.OutputQuoteCalculationResult(null);
		}
	}
}