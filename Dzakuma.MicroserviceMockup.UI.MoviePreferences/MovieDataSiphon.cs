using System.Collections.Generic;
using TMDbLib.Client;

namespace Dzakuma.MicroserviceMockup.UI.MoviePreferences
{
	public class MovieDataSiphon
	{
		private string _executablePath = @"C:\repositories\Dzakuma.MicroserviceMockup.EmployeeData\Dzakuma.MicroserviceMockup.EmployeeData\bin\Debug\Dzakuma.MicroserviceMockup.EmployeeData.exe";
		private InterprocessDataSiphon _dataSiphon = new InterprocessDataSiphon();
		private TMDbClient _movieDatabaseClient = new TMDbClient("");

		public Dictionary<string, (string title, string overview, string posterPath)> CompilePersonnelMoviePreferenceDetails(string personnelId)
		{
			var compiledPreferences = new Dictionary<string, (string title, string overview, string posterPath)>();
			var preferences = GetMoviePreferences(personnelId);

			compiledPreferences["most_favorite_movie"] = GetMovieInformation(preferences.mostFavorite);
			compiledPreferences["second_favorite_movie"] = GetMovieInformation(preferences.secondFavorite);
			compiledPreferences["most_hated_movie"] = GetMovieInformation(preferences.mostHated);

			return compiledPreferences;
		}

		public (string title, string overview, string posterPath) GetMovieInformation(int movieId)
		{
			var information = _movieDatabaseClient.SearchMovieAsync(MovieDatabaseMockup.GetMovieTitle(movieId)).Result;

			return(
				title: information.Results[0].Title,
				overview: information.Results[0].Overview,
				posterPath: information.Results[0].PosterPath
			);
		}

		public (int mostFavorite, int secondFavorite, int mostHated) GetMoviePreferences(string personnelId)
		{
			var data = _dataSiphon.DeserializeJsonString(_executablePath, $"--movie --id {personnelId}");
			var bioInformation = GetBioInformation(personnelId);
			var preferences = data.individualData;

			return (
				bioInformation.gender == "Female" ? 9001 : (int)preferences["most_favorite_movie"],
				(int)preferences["second_favorite_movie"],
				(int)preferences["most_hated_movie"]
			);
		}

		public (string name, string gender) GetBioInformation(string personnelId)
		{
			var data = _dataSiphon.DeserializeJsonString(_executablePath, $"--general --id {personnelId}");
			var employeeData = data.individualData;

			return(
				(string)employeeData["first_name"],
				(string)employeeData["gender"]
			);
		}
	}
}
