namespace Phoneword;

public partial class MainPage : ContentPage
{
    string? translatedNumber;

    public MainPage()
    {
        InitializeComponent();
    }

    private void OnTranslate(object sender, EventArgs e)
    {
        string enteredNumber = PhoneNumberText.Text;
        translatedNumber = PhonewordTranslator.ToNumber(enteredNumber);

        if (!string.IsNullOrEmpty(translatedNumber))
        {
            CallButton.IsEnabled = true;
            CallButton.Text = "Call " + translatedNumber;
            resultLabel.Text = $"Translated: {translatedNumber}";
            resultLabel.TextColor = Color.FromArgb("#0F9D58");
        }
        else
        {
            CallButton.IsEnabled = false;
            CallButton.Text = "Call";
            resultLabel.Text = "No valid phone number found.";
            resultLabel.TextColor = Color.FromArgb("#C62828");
        }
    }

    async void OnCall(object sender, System.EventArgs e)
    {
        if (await this.DisplayAlertAsync(
            "Dial a Number",
            "Would you like to call " + translatedNumber + "?",
            "Yes",
            "No"))
        {
            try
            {
                if (PhoneDialer.Default.IsSupported &&
                    !string.IsNullOrWhiteSpace(translatedNumber))
                    PhoneDialer.Default.Open(translatedNumber);
            }
            catch (ArgumentNullException)
            {
                await DisplayAlertAsync("Unable to dial", "Phone number was not valid.", "OK");
            }
            catch (Exception)
            {
                // Other error has occurred.

                await DisplayAlertAsync("Unable to dial", "Phone dialing failed.", "OK");
            }
        }
    }
}