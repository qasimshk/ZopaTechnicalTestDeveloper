namespace Zopa.Calculations.Readers
{
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;

	using CsvHelper;

	using Quotes;

	/// <summary>
	/// Implementation of <see cref="IOffersReader"/> using <see cref="CsvReader"/>.
	/// </summary>
	public class CsvReaderOffersReader : IOffersReader
	{
		public IList<Offer> ReadAll(string fileName)
		{
			using (var reader = new CsvReader(File.OpenText(fileName)))
			{
				return reader.GetRecords<Offer>().ToList();
			}
		}
	}
}