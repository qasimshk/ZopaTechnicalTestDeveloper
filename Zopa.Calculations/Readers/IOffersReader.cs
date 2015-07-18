namespace Zopa.Calculations.Readers
{
	using System.Collections.Generic;

	using Quotes;

	public interface IOffersReader
	{
		IList<Offer> ReadAll(string fileName);
	}
}