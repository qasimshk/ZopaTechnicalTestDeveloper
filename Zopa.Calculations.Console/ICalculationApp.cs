namespace Zopa.Calculations.Console
{
	using System.Collections.Generic;

	public interface ICalculationApp
	{
		void Run(IList<string> parameters);
	}
}