using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDbLib;
using TMDbLib.Client;
using TMDbLib.Objects.Movies;

namespace Dzakuma.MicroserviceMockup.UI.MoviePreferences
{
	public class MovieDataSiphon
	{
		private string _executablePath = @"C:\repositories\Dzakuma.MicroserviceMockup.EmployeeData\Dzakuma.MicroserviceMockup.EmployeeData\bin\Debug\Dzakuma.MicroserviceMockup.EmployeeData.exe";
		private InterprocessDataSiphon _dataSiphon = new InterprocessDataSiphon();
		private TMDbClient _movieDatabaseClient = new TMDbClient("");

		public Dictionary<string, ValueTuple<string, string, string>> CompilePersonnelMoviePreferenceDetails(string personnelId)
		{
			var compiledPreferences = new Dictionary<string, ValueTuple<string, string, string>>();
			var preferences = GetMoviePreferences(personnelId);

			compiledPreferences["most_favorite"] = GetMovieInformation(preferences.mostFavorite);
			compiledPreferences["second_favorite"] = GetMovieInformation(preferences.secondFavorite);
			compiledPreferences["most_hated"] = GetMovieInformation(preferences.mostHated);

			return compiledPreferences;
		}

		public (string title, string overview, string posterPath) GetMovieInformation(int movieId)
		{
			var information = _movieDatabaseClient.SearchMovieAsync(MovieDatabaseMockup.GetMovieTitle(movieId)).Result;

			return(
				information.Results[0].Title,
				information.Results[0].Overview,
				information.Results[0].PosterPath
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
