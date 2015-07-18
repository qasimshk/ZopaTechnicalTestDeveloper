namespace Zopa.Calculations.Console
{
	using System;
	using System.Globalization;

	using StructureMap;

	using Quotes;
	using Readers;

	public class Program
	{
		private static void Main(string[] args)
		{
			CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
			CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.DefaultThreadCurrentCulture;

			using (var container = ConfigureDependencies())
			{
				try
				{
					var app = container.GetInstance<ICalculationApp>();
					app.Run(args);
				}
				catch (Exception e)
				{
					Help(e.Message);
				}
			}
		}

		private static void Help(string errorText = null)
		{
			if (null != errorText)
			{
				Console.WriteLine($"Error:\n{errorText}\n------------------");
			}

			Console.WriteLine("Help: calculate.exe <market_file_csv>[string] <loan_amount>[int]");
		}

		private static IContainer ConfigureDependencies()
		{
			return new Container(x =>
			{
				x.For<IOffersReader>().Use<CsvReaderOffersReader>();
				x.For<ICalculationsOutput>().Use<SystemConsoleCalculationsOutput>();
				x.For<IQuoteCalculator>().Use<QuoteCalculator>();
				x.For<ICalculationApp>().Use<CalculationApp>();
			});
		}
	}
}
