namespace PAMobile.Views.Details;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class LoanExtractPage : ContentPage
{
	public LoanExtractPage()
	{
		InitializeComponent();

		BindingContext = new LoanExtractViewModel();
	}
}