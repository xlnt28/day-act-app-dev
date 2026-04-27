namespace Notes;

public partial class MainPage : ContentPage
{
    string _fileName = Path.Combine(FileSystem.AppDataDirectory, "notes.txt");

    public MainPage()
    {
        InitializeComponent();

        if (File.Exists(_fileName))
        {
            editor.Text = File.ReadAllText(_fileName);
            statusMessage.Text = "Loaded";
        }

        UpdateCount();
    }

    void OnSaveButtonClicked(object sender, EventArgs e)
    {
        File.WriteAllText(_fileName, editor.Text ?? string.Empty);
        statusMessage.Text = "Saved";
        statusMessage.TextColor = Color.FromArgb("#0F9D58");
    }

    void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        if (File.Exists(_fileName))
        {
            File.Delete(_fileName);
        }

        editor.Text = string.Empty;
        statusMessage.Text = "Deleted";
        statusMessage.TextColor = Color.FromArgb("#C62828");
        UpdateCount();
    }

    void OnEditorTextChanged(object sender, TextChangedEventArgs e)
    {
        UpdateCount();
    }

    void UpdateCount()
    {
        int count = editor.Text?.Length ?? 0;
        countLabel.Text = $"{count} characters";
    }
}
