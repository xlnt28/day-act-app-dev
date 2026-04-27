namespace WeatherClient;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void btnRefresh_Clicked(object sender, EventArgs e)
    {
        string postalCode = txtPostalCode.Text?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(postalCode))
        {
            lblStatus.Text = "Please enter a postal code.";
            return;
        }

        btnRefresh.IsEnabled = false;
        actIsBusy.IsVisible = true;
        actIsBusy.IsRunning = true;
        lblStatus.Text = "Fetching forecast...";

        try
        {
            BindingContext = await Services.WeatherServer.GetWeather(postalCode);
            lblStatus.Text = $"Updated for {postalCode}";
        }
        catch
        {
            lblStatus.Text = "Could not load forecast right now.";
        }
        finally
        {
            btnRefresh.IsEnabled = true;
            actIsBusy.IsRunning = false;
            actIsBusy.IsVisible = false;
        }
    }
}
