namespace PAMobile.Views.Details;

public partial class LoanDebtPage : ContentPage
{
	public LoanDebtPage()
	{
		InitializeComponent();

        BindingContext = _viewModel = new LoanDebtViewModel();
    }

    LoanDebtViewModel _viewModel;
}