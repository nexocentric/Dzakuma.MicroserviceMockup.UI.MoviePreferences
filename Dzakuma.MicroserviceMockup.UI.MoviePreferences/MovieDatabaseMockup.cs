using System.Collections.Generic;

namespace Dzakuma.MicroserviceMockup.UI.MoviePreferences
{
	public class MovieDatabaseMockup
	{
		private static readonly IList<string> _movieDatabase = new List<string>
		{
			"Eternal Sunshine of the Spotless Mind",
			"The Shawshank Redemption",
			"Pulp Fiction",
			"Forrest Gump",
			"The Dark Knight",
			"Goodfellas",
			"Star Wars Episode V The Empire Strikes Back",
			"The Matrix",
			"Indiana Jones and the Raiders of the Lost Ark",
			"Schindler's List",
			"Saving Private Ryan",
			"Gladiator",
			"The Silence of the Lambs",
			"One Flew Over the Cuckoo's",
			"Casablanca",
			"Braveheart",
			"Apocalypse Now",
			"Citizen Kane",
			"The Shining",
			"Inception",
			"The Incredibles",
			"Titanic",
			"Jaws",
			"Jurassic Park",
			"300",
			"Romeo Must Die",
			"The House of the Flying Daggers",
			"Crouching Tiger Hidden Dragon",
			"Rogue One",
			"Wall-E",
			"Gattaca",
			"Batman Forever"
		};

		public static string GetMovieTitle(int movieId)
		{
			movieId = ((movieId < 0) || (_movieDatabase.Count <= movieId)) ? _movieDatabase.Count - 1 : movieId;
			return _movieDatabase[movieId];
		}
	}
}
