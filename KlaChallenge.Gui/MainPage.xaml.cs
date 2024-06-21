using System.Globalization;

namespace KlaChallenge.Gui;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	private void OnConvertClicked(object sender, EventArgs e)
	{
		if (decimal.TryParse(numberEntry.Text, CultureInfo.CreateSpecificCulture("en-DE"), out decimal number))
		{
			var converter = new NumberConverter();
			var result = converter.Convert(number);
			resultLabel.Text = result;
		}
		else
		{
			resultLabel.Text = "Invalid input.";
		}
	}
}

