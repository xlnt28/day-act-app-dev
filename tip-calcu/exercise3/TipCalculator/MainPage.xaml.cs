namespace TipCalculator;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();

        billInput.TextChanged += (s, e) => CalculateTip(false, false);
        roundDown.Clicked += (s, e) => CalculateTip(false, true);
        roundUp.Clicked += (s, e) => CalculateTip(true, false);

        tipPercentSlider.ValueChanged += (s, e) =>
        {
            double pct = Math.Round(e.NewValue);
            tipPercent.Text = pct + "%";
            CalculateTip(false, false);
        };
    }

    void CalculateTip(bool roundUp, bool roundDown)
    {
        if (double.TryParse(billInput.Text, out double billTotal) && billTotal > 0)
        {
            double pct = Math.Round(tipPercentSlider.Value);
            double tip = Math.Round(billTotal * (pct / 100.0), 2);

            double final = billTotal + tip;

            if (roundUp)
            {
                final = Math.Ceiling(final);
                tip = final - billTotal;
            }
            else if (roundDown)
            {
                final = Math.Floor(final);
                tip = final - billTotal;
            }

            tipOutput.Text = tip.ToString("C");
            totalOutput.Text = final.ToString("C");
            return;
        }

        tipOutput.Text = 0d.ToString("C");
        totalOutput.Text = 0d.ToString("C");
    }

    void OnNormalTip(object sender, EventArgs e) => tipPercentSlider.Value = 15;

    void OnGenerousTip(object sender, EventArgs e) => tipPercentSlider.Value = 20;

    void OnAmazingTip(object sender, EventArgs e) => tipPercentSlider.Value = 25;
}
