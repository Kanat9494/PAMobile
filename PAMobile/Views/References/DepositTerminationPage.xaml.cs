namespace PAMobile.Views.References;

public partial class DepositTerminationPage : ContentPage
{
	public DepositTerminationPage()
	{
		InitializeComponent();

		BindingContext = new DepositTerminationViewModel();
	}
}