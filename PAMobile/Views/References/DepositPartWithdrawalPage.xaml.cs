namespace PAMobile.Views.References;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class DepositPartWithdrawalPage : ContentPage
{
	public DepositPartWithdrawalPage()
	{
		InitializeComponent();

		BindingContext = new DepositPartWithdrawalViewModel();
	}
}