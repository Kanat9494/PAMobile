namespace PAMobile.Views.References;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class GetLoanOverPaymentPage : ContentPage
{
	public GetLoanOverPaymentPage()
	{
		InitializeComponent();

		BindingContext = new GetLoanOverPaymentViewModel();
	}
}