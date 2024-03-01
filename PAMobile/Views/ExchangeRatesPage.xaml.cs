namespace PAMobile.Views;

public partial class ExchangeRatesPage : ContentPage
{
	public ExchangeRatesPage()
	{
		InitializeComponent();

		BindingContext = new ExchangeRatesViewModel();
	}
}