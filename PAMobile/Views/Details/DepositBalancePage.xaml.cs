namespace PAMobile.Views.Details;

public partial class DepositBalancePage : ContentPage
{
	public DepositBalancePage()
	{
		InitializeComponent();

        BindingContext = _viewModel = new DepositBalanceViewModel();
    }

    DepositBalanceViewModel _viewModel;
}