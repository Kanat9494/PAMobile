namespace PAMobile.Views.Details;

public partial class DepositReplenishmentChoosePage : ContentPage
{
	public DepositReplenishmentChoosePage()
	{
		InitializeComponent();

		BindingContext = new DepositReplenishmentChooseViewModel();
	}
}