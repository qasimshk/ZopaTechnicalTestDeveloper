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

		private readonly IOffersReader _offersReader;
		private readonly ICalculationsOutput _calculationsOutput;
		private readonly IQuoteCalculator _quoteCalculator;

		#region ctors

		public CalculationApp(IOffersReader offersReader, ICalculationsOutput calculationsOutput, IQuoteCalculator quoteCalculator)
		{
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

			this._offersReader = offersReader;
			this._calculationsOutput = calculationsOutput;
			this._quoteCalculator = quoteCalculator;
		}

		#endregion

		public void Run(IList<string> parameters)
		{
			if (null == parameters)
			{
				throw new ArgumentNullException(nameof(parameters));
			}

			if (parameters.Count < 2)
			{
				throw new ArgumentException("Invalid number of input parameters. At least two parameters expected.", nameof(parameters));
			}

			var offersFileName = parameters[0];
			var loanAmount = ParseAndValidateLoanAmount(parameters[1]);
			var offers = this._offersReader.ReadAll(offersFileName);

			var result = this._quoteCalculator.GetQuote(loanAmount, offers);

			this._calculationsOutput.OutputQuoteCalculationResult(result);
		}

		private static int ParseAndValidateLoanAmount(string loanAmountAsString)
		{
			int loanAmount;

			if (!int.TryParse(loanAmountAsString, out loanAmount))
			{
				throw new ArgumentException("Invalid value for loan amount parameter.", nameof(loanAmount));
			}

			if (loanAmount < LoanAmountMinValue || loanAmount > LoanAmountMaxValue)
			{
				throw new ArgumentOutOfRangeException(nameof(loanAmount), loanAmount, $"Loan amount must be inside the interval of {LoanAmountMinValue} to {LoanAmountMaxValue}.");
			}

			if (loanAmount % LoanAmountStep != 0)
			{
				throw new ArgumentException($"Loan amount must be dividable by {LoanAmountStep}.", nameof(loanAmount));
			}

			return loanAmount;
		}
	}
}