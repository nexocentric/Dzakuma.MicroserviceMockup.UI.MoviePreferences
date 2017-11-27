using Dzakuma.MicroserviceMockup.UI.MoviePreferences;
using Xunit;

namespace Dzakuma.MicroserviceMockup.UI.MoviePreferencesTests
{
    public class MovieDataSiphonTests
    {
	    public class CompilePersonnelMoviePreferenceDetails
	    {
		    [Fact]
		    public void ShouldReturn_DictionaryOThreeMoviePreferences_WhenCalled()
		    {
			    var testObject = new MovieDataSiphon().CompilePersonnelMoviePreferenceDetails("1");
				Assert.Equal(3, testObject.Keys.Count);
		    }

		    [Fact]
		    public void ShouldHave_MostFavoriteMovie_WhenCalled()
		    {
			    var testObject = new MovieDataSiphon().CompilePersonnelMoviePreferenceDetails("1");
			    Assert.NotEmpty(testObject["most_favorite_movie"].title);
			    Assert.NotEmpty(testObject["most_favorite_movie"].overview);
			    Assert.NotEmpty(testObject["most_favorite_movie"].posterPath);
			}

		    [Fact]
		    public void ShouldHave_SecondFavoriteMovie_WhenCalled()
		    {
			    var testObject = new MovieDataSiphon().CompilePersonnelMoviePreferenceDetails("1");
				Assert.NotEmpty(testObject["second_favorite_movie"].title);
			    Assert.NotEmpty(testObject["second_favorite_movie"].overview);
			    Assert.NotEmpty(testObject["second_favorite_movie"].posterPath);
			}

		    [Fact]
		    public void ShouldHave_MostHatedMovie_WhenCalled()
		    {
			    var testObject = new MovieDataSiphon().CompilePersonnelMoviePreferenceDetails("1");
				Assert.NotEmpty(testObject["most_hated_movie"].title);
			    Assert.NotEmpty(testObject["most_hated_movie"].overview);
			    Assert.NotEmpty(testObject["most_hated_movie"].posterPath);
			}
		}

	    public class GetMovieInformation
		{
			[Fact]
			public void ShouldReturn_TitleOverviewAndPosterPathProperties()
			{
				var testObject = new MovieDataSiphon().GetMovieInformation(1);
				Assert.NotEmpty(testObject.title);
				Assert.NotEmpty(testObject.overview);
				Assert.NotEmpty(testObject.posterPath);
			}
	    }

	    public class GetMoviePreferences
		{
			[Fact]
			public void ShouldReturn_MovieIdsForUserPreferences()
			{
				var testObject = new MovieDataSiphon().GetMoviePreferences("1");
				Assert.InRange(testObject.mostFavorite, 1, 30);
				Assert.InRange(testObject.secondFavorite, 1, 30);
				Assert.InRange(testObject.mostHated, 1, 30);
			}
		}

	    public class GetBioInformation
		{
			[Fact]
			public void ShouldReturn_MovieIdsForUserPreferences()
			{
				var testObject = new MovieDataSiphon().GetBioInformation("1");
				Assert.NotEmpty(testObject.name);
				Assert.NotEmpty(testObject.gender);
			}
		}
	}
}
