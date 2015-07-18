// ReSharper disable InconsistentNaming
namespace Zopa.Calculations.Console.UnitTest
{
	using System;
	using System.Collections.Generic;

	using Moq;

	using NUnit.Framework;

	using Quotes;
	using Readers;

	[TestFixture]
	public class CalculationAppUnitTests
	{
		[Test, ExpectedException(typeof(ArgumentNullException))]
		public void WhenInputParametersAreNull_ThrowArgumentNullException()
		{
			var offersReaderMock = new Mock<IOffersReader>();
			var calculationsOutputMock = new Mock<ICalculationsOutput>();
			var quoteCalculationMock = new Mock<IQuoteCalculator>();

			var calculationApp = new CalculationApp(
				offersReaderMock.Object,
				calculationsOutputMock.Object,
				quoteCalculationMock.Object) as ICalculationApp;

			calculationApp.Run(null);
		}

		[Test, ExpectedException(typeof(ArgumentException))]
		public void WhenNoOfInputParametersIsLowerThenTwo_ThrowArgumentException()
		{
			var offersReaderMock = new Mock<IOffersReader>();
			var calculationsOutputMock = new Mock<ICalculationsOutput>();
			var quoteCalculationMock = new Mock<IQuoteCalculator>();

			var parameters = new string[] { };

			var calculationApp = new CalculationApp(
				offersReaderMock.Object,
				calculationsOutputMock.Object,
				quoteCalculationMock.Object) as ICalculationApp;

			calculationApp.Run(parameters);
		}

		[Test]
		public void ShouldLoadOffersFromInputFile()
		{
			var offersReaderMock = new Mock<IOffersReader>();
			offersReaderMock.Setup(x => x.ReadAll(It.Is<string>(p => p == "filename")))
				.Returns(new List<Offer> { new Offer() })
				.Verifiable();

			var calculationsOutputMock = new Mock<ICalculationsOutput>();

			var quoteCalculationMock = new Mock<IQuoteCalculator>();

			var parameters = new[] { "filename", "1000" };

			var calculationApp = new CalculationApp(
				offersReaderMock.Object,
				calculationsOutputMock.Object,
				quoteCalculationMock.Object) as ICalculationApp;

			calculationApp.Run(parameters);

			offersReaderMock.Verify();
		}

		[Test]
		[TestCase(1001), TestCase(1101), TestCase(5005), TestCase(2245)]
		[ExpectedException(typeof(ArgumentException))]
		public void WhenLoanAmountIsNotDividableByHundred_ThrowArgumentException(int loanAmount)
		{
			var offersReaderMock = new Mock<IOffersReader>();
			var calculationsOutputMock = new Mock<ICalculationsOutput>();
			var quoteCalculationMock = new Mock<IQuoteCalculator>();

			var parameters = new[] { "filename", loanAmount.ToString() };

			var calculationApp = new CalculationApp(
				offersReaderMock.Object,
				calculationsOutputMock.Object,
				quoteCalculationMock.Object) as ICalculationApp;

			calculationApp.Run(parameters);
		}

		[Test]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void WhenLoanAmountIsLowerThen1000_ThrowArgumentOutOfRangeException()
		{
			var offersReaderMock = new Mock<IOffersReader>();
			var calculationsOutputMock = new Mock<ICalculationsOutput>();
			var quoteCalculationMock = new Mock<IQuoteCalculator>();

			var parameters = new[] { "filename", 101.ToString() };

			// ReSharper disable once UnusedVariable
			var calculationApp = new CalculationApp(
				offersReaderMock.Object,
				calculationsOutputMock.Object,
				quoteCalculationMock.Object) as ICalculationApp;

			calculationApp.Run(parameters);
		}

		[Test]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void WhenLoanAmountIsGreaterThen15000_ThrowArgumentOutOfRangeException()
		{
			var offersReaderMock = new Mock<IOffersReader>();
			var calculationsOutputMock = new Mock<ICalculationsOutput>();
			var quoteCalculationMock = new Mock<IQuoteCalculator>();

			var parameters = new[] { "filename", 15001.ToString() };

			// ReSharper disable once UnusedVariable
			var calculationApp = new CalculationApp(
				offersReaderMock.Object,
				calculationsOutputMock.Object,
				quoteCalculationMock.Object) as ICalculationApp;

			calculationApp.Run(parameters);
		}

		[Test]
		public void ShouldGetQuoteCalculationResult()
		{
			var offersReaderMock = new Mock<IOffersReader>();
			var offers = new List<Offer>();
			offersReaderMock.Setup(x => x.ReadAll(It.IsAny<string>())).Returns(offers);

			var calculationsOutputMock = new Mock<ICalculationsOutput>();

			var quoteCalculationMock = new Mock<IQuoteCalculator>();
			quoteCalculationMock.Setup(x => x.GetQuote(It.Is<int>(p => 1000 == p), It.Is<List<Offer>>(p => offers.Equals(p))))
				.Verifiable();

			var parameters = new[] { "filename", 1000.ToString() };

			// ReSharper disable once UnusedVariable
			var calculationApp = new CalculationApp(
				offersReaderMock.Object,
				calculationsOutputMock.Object,
				quoteCalculationMock.Object) as ICalculationApp;

			calculationApp.Run(parameters);

			quoteCalculationMock.Verify();
		}

		[Test]
		public void ShouldOutputQuoteCalculationResult()
		{
			var quoteCalculationResult = new QuoteCalculationResult();

			var offersReaderMock = new Mock<IOffersReader>();

			var calculationsOutputMock = new Mock<ICalculationsOutput>();
			calculationsOutputMock.Setup(x => x.OutputQuoteCalculationResult(It.Is<QuoteCalculationResult>(p => quoteCalculationResult.Equals(p))))
				.Verifiable();

			var quoteCalculationMock = new Mock<IQuoteCalculator>();
			quoteCalculationMock.Setup(x => x.GetQuote(It.IsAny<int>(), It.IsAny<IList<Offer>>()))
				.Returns(quoteCalculationResult);

			var parameters = new[] { "filename", 1000.ToString() };

			var calculationApp = new CalculationApp(
				offersReaderMock.Object,
				calculationsOutputMock.Object,
				quoteCalculationMock.Object) as ICalculationApp;

			calculationApp.Run(parameters);

			calculationsOutputMock.Verify();
		}

		[Test]
		public void WhenCalculationResultIsNull_OutputInsufficiendOffers()
		{
			var offersReaderMock = new Mock<IOffersReader>();
			offersReaderMock.Setup(x => x.ReadAll(It.IsAny<string>())).Returns(new List<Offer>());

			var calculationsOutputMock = new Mock<ICalculationsOutput>();
			calculationsOutputMock.Setup(x => x.InsufficiendOffers())
				.Verifiable();

			var quoteCalculationMock = new Mock<IQuoteCalculator>();

			var parameters = new[] { "filename", 1000.ToString() };

			var calculationApp = new CalculationApp(
				offersReaderMock.Object,
				calculationsOutputMock.Object,
				quoteCalculationMock.Object) as ICalculationApp;

			calculationApp.Run(parameters);

			calculationsOutputMock.Verify();
		}
	}
}
