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
		public void ShouldReturnLoanAmount()
		{
			var calculator = new QuoteCalculator() as IQuoteCalculator;

			var result = calculator.GetQuote(1000, new List<Offer> { new Offer { CashAvailable = 1000 } });

			Assert.IsNotNull(result);
			Assert.AreEqual(1000, result.LoanAmount);
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

		[Test]
		[TestCaseSource(typeof(TestCasesRepository), nameof(TestCasesRepository.TotalRepaymentTestCases))]
		public decimal ShouldCalculateTotalRepaymentForRequestedLoanAmount(int loanAmount, IList<Offer> offers)
		{
			var calculator = new QuoteCalculator() as IQuoteCalculator;

			var result = calculator.GetQuote(loanAmount, offers);

			Assert.IsNotNull(result);
			return result.TotalRepayment;
		}

		[Test]
		public void WhenThereIsNotEnoughOffersToSatisfyTheRequestedLoanAmount_ReturnNull()
		{
			var calculator = new QuoteCalculator() as IQuoteCalculator;

			var result = calculator.GetQuote(2000, new List<Offer> { new Offer { Rate = 0.5m, CashAvailable = 1000 } });

			Assert.IsNull(result);
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
								.Returns(0.40m);
					yield return
						new TestCaseData(
							1500,
							new List<Offer>
								{
									new Offer { Rate = 0.60m, CashAvailable = 1500 },
									new Offer { Rate = 0.50m, CashAvailable = 1000 },
									new Offer { Rate = 0.20m, CashAvailable = 500 },
								})
								.Returns(0.40m);
					yield return
						new TestCaseData(
							1000,
							new List<Offer>
								{
									new Offer { Rate = 0.50m, CashAvailable = 1000 },
									new Offer { Rate = 0.20m, CashAvailable = 1500 },
								})
								.Returns(0.20m);
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

			public static IEnumerable TotalRepaymentTestCases
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
								.Returns(1800m);
				}
			}
		}

		#endregion
	}
}
