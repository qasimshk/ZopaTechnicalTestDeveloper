// ReSharper disable InconsistentNaming
namespace Zopa.Calculations.UnitTest.Quotes
{
	using System;
	using System.Collections;
	using System.Collections.Generic;

	using NUnit.Framework;

	using Calculations.Quotes;

	[TestFixture]
	public class QuoteCalculatorUnitTests
	{
		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void WhenOffersAreNull_ThrowArgumentNullException()
		{
			var calculator = new QuoteCalculator() as IQuoteCalculator;

			// ReSharper disable once UnusedVariable
			var result = calculator.GetQuote(1, null);
		}

		[Test]
		[TestCaseSource(typeof(TestCasesRepository), nameof(TestCasesRepository.QuoteTestCases))]
		public decimal ShouldCalculateLowestQuoteRateFromOffersForRequestedLoanAmount(int loanAmount, IList<Offer> offers)
		{
			var calculator = new QuoteCalculator() as IQuoteCalculator;

			var result = calculator.GetQuote(loanAmount, offers);

			Assert.IsNotNull(result);
			return result.Quote;
		}

		[Test]
		[TestCaseSource(typeof(TestCasesRepository), nameof(TestCasesRepository.MonthlyRepaymentTestCases))]
		public decimal ShouldCalculateMonthlyRepaymentForRequestedLoanAmount(int loanAmount, IList<Offer> offers)
		{
			var calculator = new QuoteCalculator() as IQuoteCalculator;

			var result = calculator.GetQuote(loanAmount, offers);

			Assert.IsNotNull(result);
			return result.MonthlyRepayment;
		}

		#region Test cases data

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
							1500,
							new List<Offer>
								{
									new Offer { Rate = 0.50m, CashAvailable = 1000 },
									new Offer { Rate = 0.20m, CashAvailable = 500 }
								})
								.Returns(0.60m);
				}
			}

			public static IEnumerable MonthlyRepaymentTestCases
			{
				get
				{
					yield return
						new TestCaseData(
							1000,
							new List<Offer>
								{
									new Offer { Rate = 0.80m, CashAvailable = 1000 }
								})
								.Returns(50m);
				}
			}
		}

		#endregion
	}
}
