namespace Zopa.Calculations.Console
{
	using System;

	using Quotes;

	public class SystemConsoleCalculationsOutput : ICalculationsOutput
	{
		public void OutputQuoteCalculationResult(QuoteCalculationResult result)
		{
			if (result == null)
			{
				throw new ArgumentNullException(nameof(result));
			}

			Console.WriteLine($"Requested amount: {result.LoanAmount:c}");
			Console.WriteLine($"Rate: {result.Quote:P}");
			Console.WriteLine($"Monthly repayment: {result.MonthlyRepayment:c}");
			Console.WriteLine($"Total repayment: {result.LoanAmount:c}");
		}
	}
}