namespace MovieCatalog.Views;

public partial class MoviesListPage : ContentPage
{
    public MoviesListPage()
    {
        InitializeComponent();
    }

    private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count == 0 || e.CurrentSelection[0] is not ViewModels.MovieViewModel selectedMovie)
            return;

        App.MainViewModel.SelectedMovie = selectedMovie;
        await Navigation.PushAsync(new Views.MovieDetailPage());

        if (sender is CollectionView collectionView)
        {
            collectionView.SelectedItem = null;
        }
    }

    private void DeleteButton_Clicked(object sender, EventArgs e)
    {
        if (sender is not Button button || button.BindingContext is not ViewModels.MovieViewModel movie)
            return;

        App.MainViewModel?.DeleteMovie(movie);
    }
}
