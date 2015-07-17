namespace Zopa.Calculations.Quotes
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Linq;

	public class QuoteCalculator : IQuoteCalculator
	{
		private readonly IReadOnlyCollection<Offer> _offers;

		#region ctors

		public QuoteCalculator(IList<Offer> offers)
		{
			if (null == offers)
			{
				throw new ArgumentNullException(nameof(offers));
			}

			this._offers = new ReadOnlyCollection<Offer>(offers);
		}

		#endregion

		public QuoteCalculationResult GetQuote(int loanAmount)
		{
			var minimalRate = this._offers.Min(x => x.Rate);

			return new QuoteCalculationResult
			{
				Quote = minimalRate
			};
		}
	}
}