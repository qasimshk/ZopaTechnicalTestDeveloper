namespace Zopa.Calculations.Quotes
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Linq;

	public class QuoteCalculator : IQuoteCalculator
	{
		public const int LoanLengthInMonths = 36;

		public QuoteCalculationResult GetQuote(int loanAmount, ICollection<Offer> offers)
		{
			if (null == offers)
			{
				throw new ArgumentNullException(nameof(offers));
			}

			var quote = CalculateQuote(loanAmount, offers);

			var monthlyRepayment = loanAmount * (1 + quote) / LoanLengthInMonths;

			return new QuoteCalculationResult
			{
				Quote = quote,
				MonthlyRepayment = monthlyRepayment
			};
		}

		private static decimal CalculateQuote(int loanAmount, ICollection<Offer> offers)
		{
			var minimalRate = offers.Min(x => x.Rate);
			return minimalRate;
		}
	}
}