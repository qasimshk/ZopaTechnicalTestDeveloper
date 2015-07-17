namespace Zopa.Calculations.UnitTest
{
	using System;
	using System.Collections;
	using System.Collections.Generic;

	using NUnit.Framework;

	using Zopa.Calculations.Quotes;

	[TestFixture]
	public class QuoteCalculatorUnitTests
	{
		[Test]
		[TestCaseSource(typeof(TestCasesRepository), nameof(TestCasesRepository.QuoteTestCases))]
		public decimal ShouldCalculateLowestRateFromOffersForRequestedLoanAmount(int loanAmount, IList<Offer> offers)
		{
			var calculator = new QuoteCalculator(offers) as IQuoteCalculator;

			var result = calculator.GetQuote(loanAmount);

			Assert.IsNotNull(result);
			return result.Quote;
		}

		[Test]
		[TestCaseSource(typeof(TestCasesRepository), nameof(TestCasesRepository.QuoteTestCases))]
		public decimal ShouldCalculateMonthlyRepaymentForRequestedLoanAmount(int loanAmount, IList<Offer> offers)
		{
			var calculator = new QuoteCalculator(offers) as IQuoteCalculator;

			var result = calculator.GetQuote(loanAmount);

			Assert.IsNotNull(result);
			return result.MonthlyRepayment;
		}

		public class ExpectedResultAndTestData
		{
			public QuoteCalculationResult ExpectedResult { get; set; }

			public IList<Offer> Offers { get; set; }
		}

		public class TestCasesRepository
		{
			public static IEnumerable QuoteTestCases
			{
				get
				{
					yield return
						new TestCaseData(
							1000,
							new List<Offer>
								{
									new Offer { Rate = 0.10m, CashAvailable = 1000 }
								})
								.Returns(0.10m);
					yield return
						new TestCaseData(
							1000,
							new List<Offer>
								{
									new Offer { Rate = 0.50m, CashAvailable = 1000 },
									new Offer { Rate = 0.20m, CashAvailable = 500 }
								})
								.Returns(0.20m);
				}
			}
		}
	}
	/*
	500 za 0.2
	500 za 0.5
	*/
}
