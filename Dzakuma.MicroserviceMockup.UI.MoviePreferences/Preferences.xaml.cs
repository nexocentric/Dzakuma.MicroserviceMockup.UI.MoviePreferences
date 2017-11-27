using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Dzakuma.MicroserviceMockup.UI.MoviePreferences
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class Preferences : Window
	{
		private readonly string _personnelId;
		private readonly MovieDataSiphon _databaseConnection = new MovieDataSiphon();
		private string _executablePath = @"C:\repositories\Dzakuma.MicroserviceMockup.UI.EmployeeDashboard\Dzakuma.MicroserviceMockup.UI.EmployeeDashboard\bin\Debug\Dzakuma.MicroserviceMockup.UI.EmployeeDashboard.exe";
		private InterprocessDataSiphon _dataSiphon = new InterprocessDataSiphon();
		private IReadOnlyDictionary<string, (string title, string overview, string posterPath)> _moviePreferences;

		public Preferences(string personnelId = null)
		{
			_personnelId = personnelId ?? "6";

			InitializeComponent();
			LoadProfilePicture(_personnelId);
			LoadEmployeeName(_personnelId);
			LoadAndSetMoviePreferences();
			LoadMovieDescription("most_favorite_movie");
		}

		public void LoadAndSetMoviePreferences()
		{
			_moviePreferences = _databaseConnection.CompilePersonnelMoviePreferenceDetails(_personnelId);
			MostFavorite.Text = _moviePreferences["most_favorite_movie"].title;
			SecondFavorite.Text = _moviePreferences["second_favorite_movie"].title;
			MostHated.Text = _moviePreferences["most_hated_movie"].title;
		}

		public void LoadMovieDescription(string preferenceKey)
		{
			LoadMoviePoster(preferenceKey);
			MovieTitle.Text = _moviePreferences[preferenceKey].title;
			MovieOverview.Text = _moviePreferences[preferenceKey].overview;
		}

		public void LoadProfilePicture(string peronnelId)
		{
			var profileUrl = _dataSiphon.DecodeString(_executablePath, $"--data --id {peronnelId}");
			ProfilePicture.Source = LoadImageFromUrl(new Uri(profileUrl, UriKind.Absolute));
		}

		public void LoadEmployeeName(string personnelId)
		{
			var bio = _databaseConnection.GetBioInformation(personnelId);
			EmployeeName.Text = $"{bio.name}'s Movie Preferences";
		}

		private void LoadMoviePoster(string preferenceKey)
		{
			var posterPath = _moviePreferences[preferenceKey].posterPath;
			MoviePoster.Source = LoadImageFromUrl(new Uri($@"https://image.tmdb.org/t/p/w640{posterPath}", UriKind.Absolute));
		}

		private BitmapImage LoadImageFromUrl(Uri imageSource)
		{
			var picture = new BitmapImage();
			picture.BeginInit();
			picture.UriSource = imageSource;
			picture.EndInit();
			return picture;
		}

		private void MostFavorite_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			LoadMovieDescription("most_favorite_movie");
		}

		private void SecondFavorite_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			LoadMovieDescription("second_favorite_movie");
		}

		private void MostHated_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			LoadMovieDescription("most_hated_movie");
		}
	}
}
