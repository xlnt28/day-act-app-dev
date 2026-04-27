using People.Models;

namespace People;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await RefreshPeople();
    }

    public async void OnNewButtonClicked(object sender, EventArgs args)
    {
        string name = newPerson.Text?.Trim() ?? string.Empty;
        await App.PersonRepo.AddNewPerson(name);

        DisplayStatus(App.PersonRepo.StatusMessage);

        if (!string.IsNullOrWhiteSpace(name) && !App.PersonRepo.StatusMessage.StartsWith("Failed", StringComparison.OrdinalIgnoreCase))
        {
            newPerson.Text = string.Empty;
            await RefreshPeople();
        }
    }

    public async void OnGetButtonClicked(object sender, EventArgs args)
    {
        await RefreshPeople();
    }

    async Task RefreshPeople()
    {
        List<Person> people = await App.PersonRepo.GetAllPeople();
        peopleList.ItemsSource = people;

        if (!string.IsNullOrWhiteSpace(App.PersonRepo.StatusMessage))
        {
            DisplayStatus(App.PersonRepo.StatusMessage);
        }
    }

    void DisplayStatus(string message)
    {
        statusMessage.Text = message;

        bool hasError = message.StartsWith("Failed", StringComparison.OrdinalIgnoreCase)
            || message.Contains("required", StringComparison.OrdinalIgnoreCase);

        statusMessage.TextColor = hasError
            ? Color.FromArgb("#C62828")
            : Color.FromArgb("#0F9D58");
    }
}
