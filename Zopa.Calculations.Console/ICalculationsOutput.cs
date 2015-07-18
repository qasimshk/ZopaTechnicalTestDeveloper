namespace Zopa.Calculations.Console
{
	using Quotes;

	public interface ICalculationsOutput
	{
		void OutputQuoteCalculationResult(QuoteCalculationResult result);
	}
}