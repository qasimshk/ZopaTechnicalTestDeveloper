namespace Zopa.Calculations.Quotes
{
	using System.Collections.Generic;

	public interface IQuoteCalculator
	{
		QuoteCalculationResult GetQuote(int loanAmount, ICollection<Offer> offers);
	}
}