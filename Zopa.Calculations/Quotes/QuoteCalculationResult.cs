namespace Zopa.Calculations.Quotes
{
	public class QuoteCalculationResult
	{
		public int LoanAmount { get; set; }

		public decimal Quote { get; set; }

		public decimal MonthlyRepayment { get; set; }

		public decimal TotalRepayment { get; set; }
	}
}