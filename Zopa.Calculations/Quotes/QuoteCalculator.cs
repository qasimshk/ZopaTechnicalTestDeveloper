namespace Zopa.Calculations.Quotes
{
	using System;
	using System.Collections.Generic;
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

			if (offers.Sum(x => x.CashAvailable) < loanAmount)
			{
				return null;
			}

			var totalRepayment = CalculateTotalToPay(loanAmount, offers);

			var quote = (totalRepayment - loanAmount) / loanAmount;

			var monthlyRepayment = loanAmount * (1 + quote) / LoanLengthInMonths;

			return new QuoteCalculationResult
			{
				LoanAmount = loanAmount,
				Quote = quote,
				MonthlyRepayment = monthlyRepayment,
				TotalRepayment = totalRepayment
			};
		}

		private static decimal CalculateTotalToPay(int loanAmount, IEnumerable<Offer> offers)
		{
			var borrowed = 0;
			var totalTopay = 0m;

			foreach (var offer in offers.OrderBy(x => x.Rate))
			{
				var amountToBorrow = loanAmount < borrowed + offer.CashAvailable ? loanAmount - borrowed : offer.CashAvailable;

				totalTopay += amountToBorrow + (amountToBorrow * offer.Rate);

				if ((borrowed += amountToBorrow) >= loanAmount)
				{
					break;
				}
			}

			return totalTopay;
		}
	}
}