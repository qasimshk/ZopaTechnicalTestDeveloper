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

			Console.WriteLine($"Requested amount: {result.LoanAmount:c0}");
			Console.WriteLine($"Rate: {result.Quote:P1}");
			Console.WriteLine($"Monthly repayment: {result.MonthlyRepayment:c2}");
			Console.WriteLine($"Total repayment: {result.TotalRepayment:c2}");
		}
	}
}