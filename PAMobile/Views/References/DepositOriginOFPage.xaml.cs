namespace PAMobile.Views.References;

public partial class DepositOriginOFPage : ContentPage
{
	public DepositOriginOFPage()
	{
		InitializeComponent();

		BindingContext = new DepositOriginOFViewModel();
	}
}