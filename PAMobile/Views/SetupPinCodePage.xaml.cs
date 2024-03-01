namespace PAMobile.Views;

public partial class SetupPinCodePage : ContentPage
{
	public SetupPinCodePage()
	{
		InitializeComponent();

		BindingContext = new SetupPinCodeViewModel();
	}
}