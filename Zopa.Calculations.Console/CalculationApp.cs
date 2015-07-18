namespace Zopa.Calculations.Console
{
	using System;
	using System.Collections.Generic;

	using Quotes;
	using Readers;

	public class CalculationApp : ICalculationApp
	{
		public const int LoanAmountMinValue = 1000;
		public const int LoanAmountMaxValue = 15000;
		public const int LoanAmountStep = 100;

		private readonly int _loanAmount;
		private readonly ICollection<Offer> _offers;
		private readonly ICalculationsOutput _calculationsOutput;
		private readonly IQuoteCalculator _quoteCalculator;

		#region ctors

		public CalculationApp(string offersFileName, int loanAmount, IOffersReader offersReader, ICalculationsOutput calculationsOutput, IQuoteCalculator quoteCalculator)
		{
			if (null == offersFileName)
			{
				throw new ArgumentNullException(nameof(offersFileName));
			}

			if (null == offersReader)
			{
				throw new ArgumentNullException(nameof(offersReader));
			}

			if (null == calculationsOutput)
			{
				throw new ArgumentNullException(nameof(calculationsOutput));
			}

			if (null == quoteCalculator)
			{
				throw new ArgumentNullException(nameof(quoteCalculator));
			}

			ValidateLoanAmount(loanAmount);

			this._offers = offersReader.ReadAll(offersFileName);
			this._loanAmount = loanAmount;
			this._calculationsOutput = calculationsOutput;
			this._quoteCalculator = quoteCalculator;
		}

		#endregion

		public void Run()
		{
			var result = this._quoteCalculator.GetQuote(this._loanAmount, this._offers);

			this._calculationsOutput.OutputQuoteCalculationResult(result);
		}

		private static void ValidateLoanAmount(int loanAmount)
		{
			if (loanAmount < LoanAmountMinValue || loanAmount > LoanAmountMaxValue)
			{
				throw new ArgumentOutOfRangeException(nameof(loanAmount), loanAmount, $"Loan amount must be inside the interval of {LoanAmountMinValue} to {LoanAmountMaxValue}.");
			}

			if (loanAmount % LoanAmountStep != 0)
			{
				throw new ArgumentException($"Loan amount must be dividable by {LoanAmountStep}.", nameof(loanAmount));
			}
		}
	}
}