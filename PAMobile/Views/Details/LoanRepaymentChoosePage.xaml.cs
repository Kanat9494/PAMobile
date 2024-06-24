namespace PAMobile.Views.Details;

public partial class LoanRepaymentChoosePage : ContentPage
{
	public LoanRepaymentChoosePage()
	{
		InitializeComponent();

		BindingContext = new LoanRepaymentChooseViewModel();
	}
}